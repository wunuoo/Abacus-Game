using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;

public static class GameConfig
{
    public static string NPCFilePath = "Assets/Prefabs/NPC配置.asset";

    //static Dictionary<int, string> IDtoName_Map = new Dictionary<int, string>();
    //static Dictionary<string, int> NametoID_Map = new Dictionary<string, int>();
    public static Dictionary<string, NPC> nameToNPC_Map = new Dictionary<string, NPC>();

    static GameConfig()
    {
        NPCTable NPCtable = AssetDatabase.LoadAssetAtPath<NPCTable>(GameConfig.NPCFilePath);//读取npc配置表
        foreach(var npc in NPCtable.npcs)
        {
            nameToNPC_Map.Add(npc.name, npc);
        }
    }
}
