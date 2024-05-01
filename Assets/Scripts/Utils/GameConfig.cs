using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class GameConfig
{
    public static string ButtonSound = "button";

    public static string NPCFilePath = "NPC配置";
    public static string ToolFilePath = "道具配置";

    //static Dictionary<int, string> IDtoName_Map = new Dictionary<int, string>();
    //static Dictionary<string, int> NametoID_Map = new Dictionary<string, int>();
    public static Dictionary<string, NPC> nameToNPC_Map = new Dictionary<string, NPC>();
    public static Dictionary<int, NPC> idToNPC_Map = new Dictionary<int, NPC>();
    public static Dictionary<int, ToolInfo> idToTool_Map = new Dictionary<int, ToolInfo>();

    static GameConfig()
    {
        NPCTable NPCtable = Resources.Load<NPCTable>(GameConfig.NPCFilePath);//读取npc配置表
        foreach(var npc in NPCtable.npcs)
        {
            nameToNPC_Map.Add(npc.name, npc);
        }

        foreach (var npc in NPCtable.npcs)
        {
            idToNPC_Map.Add(npc.ID, npc);
        }

        ToolTable toolTable = Resources.Load<ToolTable>(GameConfig.ToolFilePath);//读取道具配置表
        foreach (var tool in toolTable.tools)
        {
            idToTool_Map.Add(tool.toolID, tool);
        }
    }

}
