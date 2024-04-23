using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������ñ�
[CreateAssetMenu(menuName = "������������", fileName = "��������")]
public class ToolTable : ScriptableObject
{
    public Tool[] tools;
}

[Serializable]
public class Tool
{
    [Header("ID")]
    public int ID;
    [Header("����")]
    public string name;
    [Header("ͼƬ")]
    public Sprite img;
}

