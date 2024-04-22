using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIDialog : UIBase
{
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI content;
    //public Image portrait;

    public Image[] portraits;
    int img_replacing_index;
    NPC lastSpeaker;

    int index;
    int length;

    Dialog dialog;
    public void StartDialog(Dialog dialog)
    {
        this.index = 0;
        this.dialog = dialog;
        this.length = dialog.dialogNodes.Count;
        Play(dialog.dialogNodes[index]);
    }

    void HightLight(Image img)
    {
        img.color = Color.white;
    }

    void LowLight(Image img)
    {
        img.color = Color.gray;
        
    }


    void Replace(Sprite pic)
    {
        portraits[img_replacing_index].sprite = pic;
    }

    void Play(DialogNode node)
    {
        if(lastSpeaker == null || node.name != lastSpeaker.name)//˵���߸ı�
        {
            lastSpeaker = GameConfig.nameToNPC_Map[node.name];
            characterName.text = node.name;

            Replace(lastSpeaker.portrait);
            HightLight(portraits[img_replacing_index]);
            portraits[img_replacing_index].gameObject.SetActive(true);
            img_replacing_index = 1 - img_replacing_index;//˵���߸ı䣬��ô�´� �� ���滻��Ӧ���Ǳ���û�滻����
            LowLight(portraits[img_replacing_index]);
        }

        content.text = node.content;
    }

    public void OnClickNext()
    {
        index++;
        if(index == length)//˵���Ѿ��������һ��Ի��е����һ����
        {
            //gameObject.SetActive(false);
            this.OnClose();
            DialogManager.Instance.OnDialogFinish();
        }
        else
        {
            Play(dialog.dialogNodes[index]);
        }
    }

    //public void OnDestroy()
    //{
    //    UIManager.Instance.DeleteInstance(this.gameObject);
    //}
}
