using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "创建任务", fileName = "任务")]
public class Task : ScriptableObject
{
    public int taskID;
    public int[] results;//中间结果
    public int nowIndex = 0;//目前所处的中间结果序号

}
