using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;



//���Manager�ƿ�����Ϸ�ľ�����ȣ��������
public class ChapterManager : MonoSingleton<ChapterManager>
{
    public bool canGoNextChapter = false;

    public List<Chapter> chapters = new List<Chapter>();
    public Chapter currentChapter;

    //��Щָ������������Chapter�У�������ΪScriptableObj��Ȼ�ڲ���󲻻ᱣ��ֵ�����ڱ༭���ᣬ���µ��ǳ��鷳
    public int chapterIndex = 0;
    public int dialogIndex = 0;
    public int taskIndex = 0;

    public UnityEvent OnLoadFinish = new UnityEvent();
    public UnityEvent OnNewChapterStart = new UnityEvent();

    public Dictionary<int, bool> envItemShining = new Dictionary<int, bool> { { 0, true }, { 1, true }, { 2, true } };

    // ���������������Ϸ�Ŀ�ʼ
    protected override void OnStart()
    {
        if(SaveManager.Instance.currentSave == null)
        {
            Debug.Log("����Ϸ��ʼ");
            CGManager.Instance.OnBlackMuskFaded.AddListener(() => {
                CGManager.Instance.ShowNextImage();
            });
            //SceneManager.Instance.loadCompleted.AddListener(() => { StartChapter(0); });
            StartChapter(0);
        }
        else
        {
            StartBySave(SaveManager.Instance.currentSave);
        }
    }

    //һ���½ڵĻ��������ǣ������Ի� --�� �������� --�� ������� --�� �ɹ��Ի� --�� �������� ������ --�� �����½ں���� --�� ��һ�¿����Ի�
    public void StartChapter(int index)
    {
        currentChapter = chapters[index];
        dialogIndex = 0;
        taskIndex = 0;
        canGoNextChapter = false;
        CGManager.Instance.OnBlackMuskFaded.AddListener(() => {
            AssignNewDialog();
        });
        if (currentChapter.hasOpenCG)
        {
            CGManager.Instance.OnBlackMuskFaded.AddListener(() => {
                CGManager.Instance.ShowNextImage();
            });
        }
        CGManager.Instance.PlayChapterBegin(currentChapter, chapterIndex);//�����Ի�

        OnNewChapterStart?.Invoke();
    }

    public void StartBySave(Save save)
    {

        Debug.Log("��ȡ�浵��������");

        RecordManager.Instance.recordsUnlockIndex = save.recordIndex;
        ToolManager.Instance.toolGotten = save.toolGottenTable;
        CharInfoManager.Instance.npcMeet = save.npcMeetTable;

        CGManager.Instance.pptIndex = save.pptIndex;
        CGManager.Instance.showingPPT = save.showingPPT;
        CGManager.Instance.Refresh();

        chapterIndex = save.chapterIndex;
        dialogIndex = save.dialogIndex;
        DialogManager.Instance.nodeIndex = save.dialogNodeIndex;
        taskIndex = save.taskIndex;

        currentChapter = chapters[chapterIndex];


        EventManager.Instance.gameStatus = save.gamestatus;
        switch (EventManager.Instance.gameStatus)
        {
            case GameStatus.None:
                break;
            case GameStatus.OnTask:
                taskIndex--;//���assign��������ָ��++��ʱ���й�
                AssignNewTask();
                break;
            case GameStatus.OnDialog:
                dialogIndex--;
                AssignNewDialog();
                break;
            case GameStatus.OnChapterFinish:
                canGoNextChapter = true;
                EventManager.Instance.OnChapterFinish?.Invoke();
                break;
            default:
                break;
        }

        OnLoadFinish?.Invoke();
    }

    internal void OnGameEnd()
    {
        StartCoroutine(ShowPics());
        IEnumerator ShowPics()
        {
            CGManager.Instance.ShowNextImage();
            yield return new WaitForSeconds(2f);
            UIManager.Instance.Show<UIEnd>();
        }
    }


    public void StartNewChapter()
    {
        if (canGoNextChapter)
        {
            chapterIndex++;
            StartChapter(chapterIndex);
        }
        else
        {
            Debug.Log("����ǰ����һ�£�");
        }
    }

    internal void AssignNewDialog()
    {
        EventManager.Instance.gameStatus = GameStatus.OnDialog;
        DialogManager.Instance.PlayDialog(currentChapter.dialogs[dialogIndex]);
        dialogIndex++;
    }

    internal void AssignSceneChangeDialog()
    {
        AssignNewDialog();
        SceneManager.Instance.loadCompleted.RemoveListener(AssignSceneChangeDialog);
    }

    internal void OnChapterFinished()
    {
        canGoNextChapter = true;
        
    }

    internal void AssignNewTask()
    {
        TaskManager.Instance.StartNewTask(currentChapter.tasks[taskIndex]);
        taskIndex++;
    }



}
