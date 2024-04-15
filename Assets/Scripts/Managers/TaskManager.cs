using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//���Manager�ƿ�����Ϸ�ľ�����ȣ��������
public class TaskManager : MonoSingleton<TaskManager>
{
    public event EventHandler<Task> NewTaskSet;
    public event EventHandler TaskPartFinished;

    public Task[] tasks;
    int taskIndex;//��ǰ������������ţ���־�˾���Ľ���

    Task currentTask;

    internal void CheckResult(int value)
    {
        int index = currentTask.nowIndex;
        if (value == currentTask.results[index])
        {
            index++;
            if(index == currentTask.results.Length)//��˵������������
            {
                this.TaskFinish();
            }
            TaskPartFinished?.Invoke(this, EventArgs.Empty);
            currentTask.nowIndex = index;
        }
    }

    private void TaskFinish()
    {
        Debug.Log("�������");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void StartNewTask(int taskID)
    {
        taskIndex = taskID;
        this.currentTask = tasks[taskID];

        NewTaskSet?.Invoke(this, currentTask);
    }
}
