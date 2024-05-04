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
            if (kv.Key != 0 && kv.Key != 9)//0是旁白，9是官员。虽然写死不是很好，但是为了效率先这么做了
            {
                npcMeet.Add(kv.Value.ID, false);
            }

        }
        npcMeet[1] = true;//1是主角，10是主角父亲，开局已知
        npcMeet[10] = true;
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
