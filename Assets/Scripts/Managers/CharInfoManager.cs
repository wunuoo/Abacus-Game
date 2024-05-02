using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharInfoManager : Singleton<CharInfoManager>
{
    public Dictionary<int, bool> npcMeet = new Dictionary<int, bool>();

    public UnityEvent OnCharInfoChange = new UnityEvent();

    public CharInfoManager()
    {
        foreach (var kv in GameConfig.idToNPC_Map)
        {
            if (kv.Key != 0)
            {
                npcMeet.Add(kv.Value.ID, false);
            }

        }
        npcMeet[1] = true;
    }

    public void MeetNPC(int ID)
    {
        if (npcMeet.ContainsKey(ID))
        {
            Debug.Log("主角获取了NPC信息：" + ID);
            npcMeet[ID] = true;
            OnCharInfoChange?.Invoke();
        }
        else
        {
            Debug.LogError("NPC不存在：" + ID);
        }
    }
}
