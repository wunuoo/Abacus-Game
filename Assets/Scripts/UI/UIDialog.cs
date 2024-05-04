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

    public Image toolDisplayBar;//չʾ��õĵ���

    int index;
    int length;//��ǰ���ŶԻ��εľ���

    public float defaultTextSpeed;//���������ʼֵ
    float textSpeed;
    bool isSettingContent;

    private void Start()
    {
        ChapterManager.Instance.OnNewChapterStart.AddListener(ResetPortrait);
    }
    private new void OnDestroy()
    {
        ChapterManager.Instance.OnNewChapterStart.RemoveListener(ResetPortrait);
    }

    public void StartDialog(Dialog dialog)
    {
        textSpeed = defaultTextSpeed;
        DontDestroyOnLoad(this.gameObject);
        gameObject.SetActive(true);

        this.index = DialogManager.Instance.nodeIndex;
        this.length = dialog.dialogNodes.Count;
        PlayCurrentNode();
    }

    //�µĶԻ�����Ф��
    private void ResetPortrait()
    {
        lastSpeaker = null;
        img_replacing_index = 0;
        foreach (var item in portraits)
        {
            item.gameObject.SetActive(false);
        }
    }


    void Replace(Sprite pic)
    {
        portraits[img_replacing_index].sprite = pic;
        portraits[img_replacing_index].SetNativeSize();
    }

    void PlayCurrentNode()
    {
        DialogNode node = DialogManager.Instance.currentNode;
        if (node.name == "�԰�")
        {
            characterName.text = node.name;
        }
        else if(lastSpeaker == null || node.name != lastSpeaker.name)//˵���߸ı�
        {
            lastSpeaker = GameConfig.nameToNPC_Map[node.name];
            characterName.text = node.name;

            Replace(lastSpeaker.portrait);
            GameUtil.HightLight(portraits[img_replacing_index]);
            portraits[img_replacing_index].gameObject.SetActive(true);
            img_replacing_index = 1 - img_replacing_index;//˵���߸ı䣬��ô�´� �� ���滻��Ӧ���Ǳ���û�滻����
            GameUtil.LowLight(portraits[img_replacing_index]);
        }
        StartCoroutine(SetContent(node.content));

        if (node.toolID != 0)
        {
            ToolInfo toolDisplay = GameConfig.idToTool_Map[node.toolID];
            ShowTool(toolDisplay.toolImage);
            toolDisplayBar.gameObject.SetActive(true);
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
        //Debug.Log("��ǰindex��" + index.ToString() + "  ���Ի����ȣ�" + length);
        if (isSettingContent)
        {
            textSpeed = 0;//���ٲ���Ի�
        }
        else
        {
            textSpeed = defaultTextSpeed;
            index++;
            if (index == length)//˵���Ѿ��������һ��Ի��е����һ����
            {
                index = 0;
                gameObject.SetActive(false);
                //this.OnClose();
                DialogManager.Instance.OnDialogFinish();
                
            }
            else
            {
                DialogManager.Instance.OnDialogNodeFinish(index);
                PlayCurrentNode();
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
        if (this.gameObject.activeSelf && Input.GetKey(KeyCode.R))
        {
            this.OnClickNext();
        }
    }
}
