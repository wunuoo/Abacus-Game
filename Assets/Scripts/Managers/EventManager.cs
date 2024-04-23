using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public Action OnChapterFinish;

    //����������������Ӧ�¼�
    public void TriggerEvent(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1://����һ������
                this.StartNewTask();
                break;
            case 2:
                this.StartNewDialog();
                break;
            case 3:
                this.ChapterFinish();
                break;
            case 4://�л������̳�������������ʱ�򴥷���һ�ζԻ�
                this.ShowSuanPan();
                break;
            case 5:
                this.GiveBooks();
                break;
            default:
                Debug.LogError("ָ�����¼���" + index + " �����ڣ�");
                break;

        }
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
            
            ChapterManager.Instance.AssignNewDialog();
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
