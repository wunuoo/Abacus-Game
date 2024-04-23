using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//道具配置表
[CreateAssetMenu(menuName = "创建道具配置", fileName = "道具配置")]
public class ToolTable : ScriptableObject
{
    public Tool[] tools;
}

[Serializable]
public class Tool
{
    [Header("ID")]
    public int ID;
    [Header("名字")]
    public string name;
    [Header("图片")]
    public Sprite img;
}

