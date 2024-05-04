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

    //ÿ�����鲦�����ͻᴥ��һ���������
    internal void CheckResult(int value)
    {
        if (value == currentTask.results[resultIndex])
        {
            resultIndex++;
            if(resultIndex == currentTask.results.Length)//��˵������������
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
        //Debug.Log("�������");
        EventManager.Instance.TriggerEvent(currentTask.eventIndex);
        currentTask = null;


    }

    public void StartNewTask(Task task)
    {
        
        this.currentTask = task;

        NewTaskSet?.Invoke();
    }
}
