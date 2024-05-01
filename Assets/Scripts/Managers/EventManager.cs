using System;
using System.Collections;
using System.Collections.Generic;
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

    //����������������Ӧ�¼�
    public void TriggerEvent(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1://����һ������
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
            case 4://�л������̳�������������ʱ�򴥷���һ�ζԻ�
                gameStatus = GameStatus.OnDialog;
                this.ShowSuanPan();
                break;
            case 5:
                gameStatus = GameStatus.OnDialog;
                this.GiveBooks();
                break;
            case 6:
                this.CheckMap();
                break;
            default:
                Debug.LogError("ָ�����¼���" + index + " �����ڣ�");
                break;

        }
    }

    private void CheckMap()
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
        UIMain.Instance.OnClickSuanPan();
        UIMain.Instance.button_Back.onClick.AddListener(() => {
            SceneManager.Instance.loadCompleted.AddListener(ChapterManager.Instance.AssignSceneChangeDialog);

            UIMain.Instance.button_Back.onClick.RemoveAllListeners();
        });
    }

    private void ChapterFinish()
    {
        RecordManager.Instance.GetRecord();
        ChapterManager.Instance.OnChapterFinished();
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
