using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//道具配置表
[CreateAssetMenu(menuName = "创建道具配置", fileName = "道具配置")]
public class ToolTable : ScriptableObject
{
    public ToolInfo[] tools;
}


