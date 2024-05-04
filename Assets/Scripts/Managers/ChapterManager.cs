using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;



//这个Manager掌控了游戏的剧情进度，相关属性
public class ChapterManager : MonoSingleton<ChapterManager>
{
    public bool canGoNextChapter = false;

    public List<Chapter> chapters = new List<Chapter>();
    public Chapter currentChapter;

    //这些指针放在这里而非Chapter中，这是因为ScriptableObj虽然在部署后不会保存值，但在编辑器会，重新调非常麻烦
    public int chapterIndex = 0;
    public int dialogIndex = 0;
    public int taskIndex = 0;

    public UnityEvent OnLoadFinish = new UnityEvent();
    public UnityEvent OnNewChapterStart = new UnityEvent();

    public Dictionary<int, bool> envItemShining = new Dictionary<int, bool> { { 0, true }, { 1, true }, { 2, true } };

    // 这个函数是整个游戏的开始
    protected override void OnStart()
    {
        if(SaveManager.Instance.currentSave == null)
        {
            Debug.Log("新游戏开始");
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

    //一个章节的基本流程是：开场对话 --》 分配任务 --》 完成任务 --》 成功对话 --》 分配任务 。。。 --》 进入章节后空闲 --》 下一章开场对话
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
        CGManager.Instance.PlayChapterBegin(currentChapter, chapterIndex);//开场对话

        OnNewChapterStart?.Invoke();
    }

    public void StartBySave(Save save)
    {

        Debug.Log("读取存档————");

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
                taskIndex--;//这和assign函数里面指针++的时机有关
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
            Debug.Log("不能前往下一章！");
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
