using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "����������Ϣ", fileName = "����")]
public class ToolInfo : ScriptableObject
{
    [Header("��������")]
    public string toolName;

    [TextArea, Header("������Ϣ")]
    public string toolDescription;
    
    [Header("����ͼƬ")]
    public Sprite toolImage;

    [Header("��ƷID")]
    public int toolID;
}


