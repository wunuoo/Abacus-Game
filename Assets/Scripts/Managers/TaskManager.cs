using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//这个Manager掌控了游戏的剧情进度，相关属性
public class TaskManager : MonoSingleton<TaskManager>
{
    public event EventHandler<Task> NewTaskSet;
    public event EventHandler TaskPartFinished;

    public Task[] tasks;
    int taskIndex;//当前所处的任务序号，标志了剧情的进度

    Task currentTask;

    internal void CheckResult(int value)
    {
        int index = currentTask.nowIndex;
        if (value == currentTask.results[index])
        {
            index++;
            if(index == currentTask.results.Length)//这说明任务做完了
            {
                this.TaskFinish();
            }
            TaskPartFinished?.Invoke(this, EventArgs.Empty);
            currentTask.nowIndex = index;
        }
    }

    private void TaskFinish()
    {
        Debug.Log("任务完成");
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
