using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TaskProceedType
{
    PartFinish,
    Finish,

}


public class TaskManager : Singleton<TaskManager>
{
    public UnityEvent TaskStatusChange = new UnityEvent();
    public UnityEvent NewTaskSet = new UnityEvent();

    public Task currentTask;
    public int resultIndex;

    //每当算珠拨动，就会触发一次这个函数
    internal void CheckResult(int value)
    {
        if (value == currentTask.results[resultIndex])
        {
            resultIndex++;
            if(resultIndex == currentTask.results.Length)//这说明任务做完了
            {
                this.TaskFinish();
            }
            TaskStatusChange?.Invoke();
        }
    }

    private void TaskFinish()
    {
        resultIndex = 0;
        TaskStatusChange?.Invoke();
        //Debug.Log("任务完成");
        EventManager.Instance.TriggerEvent(currentTask.eventIndex);
        currentTask = null;


    }

    public void StartNewTask(Task task)
    {
        
        this.currentTask = task;

        NewTaskSet?.Invoke();
    }
}
