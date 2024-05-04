using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharInfo : UIBase
{
    public TextMeshProUGUI charName;
    public TextMeshProUGUI charDescription;
    public List<UINPCElement> charImages;

    public UINPCElement prefab;
    public Transform root;

    private void Start()
    {
        foreach (var kv in CharInfoManager.Instance.npcMeet)
        {
            if (kv.Value == true)//Ϊture����ʾ�Ѿ���ȡ�˴�npc��Ϣ
            {
                UINPCElement elm = GameObject.Instantiate<UINPCElement>(prefab, root);
                elm.charInfo = GameConfig.idToNPC_Map[kv.Key];
                elm.owner = this;
                elm.SetPortrait();
                charImages.Add(elm);
            }
        }
        OnClickCharPic(charImages[0]);

        Refresh();
    }

    public void OnClickCharPic(UINPCElement elm)//���NPCͼƬ��ʾ��Ӧ��Ϣ
    {
        charName.text = "<rotate=90>" + elm.charInfo.name;
        charDescription.text = elm.charInfo.description;

        foreach (var ui in charImages)
        {
            if(ui == elm)
            {
                GameUtil.HightLight(ui.portrait);
            }
            else
            {
                GameUtil.LowLight(ui.portrait);
            }
        }
    }

    void Refresh()
    {


    }

}
