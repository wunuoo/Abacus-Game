using System;
using UnityEngine;

public enum GameStatus
{
    None,
    OnTask,
    OnDialog,
    OnChapterFinish,
}

public class EventManager : Singleton<EventManager>
{

    public GameStatus gameStatus;
    public Action OnChapterFinish;

    //根据索引，触发对应事件
    public void TriggerEvent(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1://布置一个任务
                gameStatus = GameStatus.OnTask;
                this.StartNewTask();
                break;
            case 2:
                gameStatus = GameStatus.OnDialog;
                this.StartNewDialog();
                break;
            case 3:
                gameStatus = GameStatus.OnChapterFinish;
                this.ChapterFinish();
                break;
            case 4://切换到算盘场景，当按返回时候触发下一段对话
                gameStatus = GameStatus.OnDialog;
                this.ShowSuanPan();
                break;
            case 5:
                gameStatus = GameStatus.OnDialog;
                this.GiveBooks();
                break;
            case 6:
                this.ChangeSite();
                break;
            case 7:
                this.PlayNewCG();
                break;
            case 8:
                this.HideBG();
                break;
            case 9:
                this.GameEnd();
                break;
            case 10:
                this.GameEnd();
                break;
            default:
                Debug.LogError("指定的事件：" + index + " 不存在！");
                break;

        }
    }

    private void GameEnd()
    {
        UIMain.Instance.gameObject.SetActive(false);
        ChapterManager.Instance.OnGameEnd();
        
    }



    private void HideBG()
    {
        CGManager.Instance.HidePPT();
        ChapterManager.Instance.AssignNewDialog();
    }

    private void PlayNewCG()
    {
        CGManager.Instance.ShowNextImage();
        ChapterManager.Instance.AssignNewDialog();
    }

    private void ChangeSite()
    {
        throw new NotImplementedException();
    }

    private void GiveBooks()
    {
        ToolManager.Instance.GiveTool(2);
        ToolManager.Instance.GiveTool(3);
        ChapterManager.Instance.AssignNewDialog();
    }

    private void ShowSuanPan()
    {
        SceneManager.Instance.loadCompleted.AddListener(ChapterManager.Instance.AssignSceneChangeDialog);

        UIMain.Instance.OnClickSuanPan();
    }

    private void ChapterFinish()
    {
        RecordManager.Instance.GetRecord();
        ChapterManager.Instance.OnChapterFinished();

        if (!SceneManager.Instance.IsOnMain())
            UIMain.Instance.OnClickBack();

        this.OnChapterFinish?.Invoke();
    }

    private void StartNewDialog()
    {
        //yield return new WaitForSeconds(1f);
        ChapterManager.Instance.AssignNewDialog();
    }

    private void StartNewTask()
    {
        ChapterManager.Instance.AssignNewTask();
    }
}
