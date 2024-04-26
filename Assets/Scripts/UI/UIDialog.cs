using System;
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

    public Image toolDisplayBar;//展示获得的道具

    int index;
    int length;//当前播放对话段的句数

    public float defaultTextSpeed;//用来保存初始值
    float textSpeed;
    bool isSettingContent;

    Dialog dialog;
    public void StartDialog(Dialog dialog)
    {
        textSpeed = defaultTextSpeed;
        DontDestroyOnLoad(this.gameObject);
        gameObject.SetActive(true);
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
        portraits[img_replacing_index].SetNativeSize();
    }

    void Play(DialogNode node)
    {
        if (node.name == "旁白")
        {
            characterName.text = node.name;
        }
        else if(lastSpeaker == null || node.name != lastSpeaker.name)//说话者改变
        {
            lastSpeaker = GameConfig.nameToNPC_Map[node.name];
            characterName.text = node.name;

            Replace(lastSpeaker.portrait);
            HightLight(portraits[img_replacing_index]);
            portraits[img_replacing_index].gameObject.SetActive(true);
            img_replacing_index = 1 - img_replacing_index;//说话者改变，那么下次 该 被替换的应该是本次没替换掉的
            LowLight(portraits[img_replacing_index]);
        }
        StartCoroutine(SetContent(node.content));

        if (node.toolID != 0)
        {
            ToolInfo toolDisplay = GameConfig.idToTool_Map[node.toolID];
            ShowTool(toolDisplay.toolImage);
            toolDisplayBar.gameObject.SetActive(true);

            ToolManager.Instance.GiveTool(node.toolID);
        }
        else
        {
            toolDisplayBar.gameObject.SetActive(false);
        }

        
    }

    IEnumerator SetContent(string fullContent)
    {
        isSettingContent = true;
        content.text = "";
        for (int i = 0; i < fullContent.Length; i++)
        {
            content.text += fullContent[i];
            yield return new WaitForSeconds(textSpeed);
        }
        isSettingContent = false;
    }

    private void ShowTool(Sprite img)
    {
        toolDisplayBar.sprite = img;
        toolDisplayBar.SetNativeSize();
    }

    public void OnClickNext()
    {
        //Debug.Log("当前index：" + index.ToString() + "  本对话长度：" + length);
        if (isSettingContent)
        {
            textSpeed = 0;//快速播完对话
        }
        else
        {
            textSpeed = defaultTextSpeed;
            index++;
            if (index == length)//说明已经是在最后一句对话中点击下一句了
            {

                gameObject.SetActive(false);
                //this.OnClose();
                DialogManager.Instance.OnDialogFinish();
            }
            else
            {
                Play(dialog.dialogNodes[index]);
            }
        }

    }

    //public void OnDestroy()
    //{
    //    UIManager.Instance.DeleteInstance(this.gameObject);
    //}

    private void Update()
    {
        if (this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            this.OnClickNext();
        }
    }
}
