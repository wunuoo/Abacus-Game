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
            case 1:
                this.StartNewTask();
                break;
            case 2:
                this.StartNewDialog();
                break;
            case 3:
                this.ChapterFinish();
                break;
            case 4:
                this.ShowSuanPan();
                break;
            default:
                Debug.LogError("ָ�����¼���" + index + " �����ڣ�");
                break;

        }
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
