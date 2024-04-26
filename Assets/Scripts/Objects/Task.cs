using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "创建任务", fileName = "任务")]
public class Task : ScriptableObject
{
    [Header("任务ID")]
    public int taskID;

    [Header("任务中间结果")]
    public int[] results;//中间结果

    [Header("任务触发事件")]
    public int eventIndex = 2;//一般情况下触发2号事件：发生对话

    [TextArea, Header("任务简报")]
    public string report;//任务简报
}
