using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//npc配置表
[CreateAssetMenu(menuName = "创建NPC配置", fileName = "NPC配置")]
public class NPCTable : ScriptableObject
{
    public NPC[] npcs;
}

[Serializable]
public class NPC
{
    [Header("ID")]
    public int ID;
    [Header("名字")]
    public string name;
    [Header("描述信息"),TextArea]
    public string description;
    [Header("肖像")]
    public Sprite portrait;

}

