using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToolManager : MonoSingleton<ToolManager>
{
    public List<ToolInfo> tools;

    public Dictionary<int, bool> toolGotten = new Dictionary<int, bool>();

    public UnityEvent OnToolChange = new UnityEvent();

    protected override void OnStart()
    {
        foreach(var toolInfo in tools)
        {
            toolGotten.Add(toolInfo.toolID, false);
        }
    }

    public void GiveTool(int ID)
    {
        if (toolGotten.ContainsKey(ID))
        {
            Debug.Log("获得物品：" + ID);
            toolGotten[ID] = true;
            OnToolChange?.Invoke();
        }
        else
        {
            Debug.LogError("物品不存在：" + ID);
        }
    }
}
