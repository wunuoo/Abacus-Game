using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public Action OnChapterFinish;

    //根据索引，触发对应事件
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
            default:
                Debug.LogError("指定的事件：" + index + " 不存在！");
                break;

        }
    }

    private void ChapterFinish()
    {
        ChapterManager.Instance.OnChapterFinished();
        this.OnChapterFinish?.Invoke();
    }

    private void StartNewDialog()
    {
        ChapterManager.Instance.AssignNewDialog();
    }

    private void StartNewTask()
    {
        ChapterManager.Instance.AssignNewTask();
    }
}
