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
    public PlayableDirector director;
    public Image ppt;

    public bool canGoNextChapter = false;

    public List<Chapter> chapters = new List<Chapter>();
    Chapter currentChapter;

    //这些指针放在这里而非Chapter中，这是因为ScriptableObj虽然在部署后不会保存值，但在编辑器会，重新调非常麻烦
    public int chapterIndex = 0;
    public int dialogIndex = 0;
    public int taskIndex = 0;

    // Start is called before the first frame update
    protected override void OnStart()
    {
        if(SaveManager.Instance.currentSave == null)
        {
            Debug.Log("新游戏开始");
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

        Debug.Log("读取存档————");

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
    }

    void OnBeginAnimStop(object arg)
    {
        ppt.gameObject.SetActive(false);
        this.StartChapter(0);
        director.stopped -= OnBeginAnimStop;
    }

    //一个章节的基本流程是：开场对话 --》 分配任务 --》 完成任务 --》 成功对话 --》 分配任务 。。。 --》 进入章节后空闲 --》 下一章开场对话
    void StartChapter(int index)
    {
        currentChapter = chapters[index];
        dialogIndex = 0;
        taskIndex = 0;
        canGoNextChapter = false;
        AssignNewDialog();//开场对话
        
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
