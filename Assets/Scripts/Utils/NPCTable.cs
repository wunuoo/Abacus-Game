using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//npc���ñ�
[CreateAssetMenu(menuName = "����NPC����", fileName = "NPC����")]
public class NPCTable : ScriptableObject
{
    public NPC[] npcs;
}

[Serializable]
public class NPC
{
    [Header("ID")]
    public int ID;
    [Header("����")]
    public string name;
    [Header("Ф��")]
    public Sprite portrait;
}

