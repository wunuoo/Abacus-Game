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
    public PlayableDirector director;
    public Image ppt;

    public bool canGoNextChapter = false;

    public List<Chapter> chapters = new List<Chapter>();
    Chapter currentChapter;

    //��Щָ������������Chapter�У�������ΪScriptableObj��Ȼ�ڲ���󲻻ᱣ��ֵ�����ڱ༭���ᣬ���µ��ǳ��鷳
    public int chapterIndex = 0;
    public int dialogIndex = 0;
    public int taskIndex = 0;

    // Start is called before the first frame update
    protected override void OnStart()
    {
        if(SaveManager.Instance.currentSave == null)
        {
            Debug.Log("����Ϸ��ʼ");
            ppt.gameObject.SetActive(true);
            director.stopped += OnBeginAnimStop;
            director.Play();
        }
        else
        {
            StartBySave(SaveManager.Instance.currentSave);
        }
    }

    public void StartBySave(Save save)
    {

        Debug.Log("��ȡ�浵��������");

        RecordManager.Instance.recordsUnlockIndex = save.recordIndex;
        ToolManager.Instance.toolGotten = save.toolGottenTable;

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
    }

    void OnBeginAnimStop(object arg)
    {
        ppt.gameObject.SetActive(false);
        this.StartChapter(0);
        director.stopped -= OnBeginAnimStop;
    }

    //һ���½ڵĻ��������ǣ������Ի� --�� �������� --�� ������� --�� �ɹ��Ի� --�� �������� ������ --�� �����½ں���� --�� ��һ�¿����Ի�
    void StartChapter(int index)
    {
        currentChapter = chapters[index];
        dialogIndex = 0;
        taskIndex = 0;
        canGoNextChapter = false;
        AssignNewDialog();//�����Ի�
        
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
