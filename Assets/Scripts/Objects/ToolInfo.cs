using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "创建工具信息", fileName = "工具")]
public class ToolInfo : ScriptableObject
{
    [Header("工具名称")]
    public string toolName;

    [TextArea, Header("工具信息")]
    public string toolDescription;
    
    [Header("工具图片")]
    public Sprite toolImage;

    [Header("物品ID")]
    public int toolID;
}


