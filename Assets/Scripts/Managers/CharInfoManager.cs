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
            if (kv.Key != 0 && kv.Key != 9)//0���԰ף�9�ǹ�Ա����Ȼд�����Ǻܺã�����Ϊ��Ч������ô����
            {
                npcMeet.Add(kv.Value.ID, false);
            }

        }
        npcMeet[1] = true;//1�����ǣ�10�����Ǹ��ף�������֪
        npcMeet[10] = true;
    }

    public void MeetNPC(int ID)
    {
        if (npcMeet.ContainsKey(ID))
        {
            Debug.Log("���ǻ�ȡ��NPC��Ϣ��" + ID);
            npcMeet[ID] = true;
            OnCharInfoChange?.Invoke();
        }
        else
        {
            Debug.LogError("NPC�����ڣ�" + ID);
        }
    }
}
