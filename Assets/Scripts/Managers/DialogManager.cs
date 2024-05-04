using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogNode
{
    [Header("˵����")]
    public string name;//����Ǹ��˿��ģ�����ֱ�Ӷ�ȡID
    [Header("˵����ID")]
    public int ID;
    [Header("Ф��")]
    public Sprite portrait;//����Ǹ��˿��ģ�����ֱ�Ӷ�ȡID��Ҳ�ṩ���ֶ�΢���Ŀռ�
    [TextArea, Header("˵������")]
    public string content;
    [Header("����ID")]
    public int toolID = 0;//�����0������ʾ���ߣ�������ʾ��Ӧ����
    [Header("NPC��Ϣ")]
    public int npcID = 0;//�����0������Ӧ�������ȡ��ӦNPC��Ϣ
}

public class DialogManager : MonoSingleton<DialogManager>
{
    bool speaking = false;

    Dialog currentDialog;

    public DialogNode currentNode;
    public int nodeIndex;

    //�����ChapterManager������ָ��
    public void PlayDialog(Dialog dialog)
    {
        currentDialog = dialog;
        currentNode = dialog.dialogNodes[nodeIndex];

        UIDialog ui = UIManager.Instance.Show<UIDialog>();
        ui.StartDialog(dialog);
        nodeIndex++;

        speaking = true;
        SceneManager.Instance.DisableAllColliders(speaking, this.gameObject);

    }

    //���û�������һ����һ�䰴ť�󣬴����������
    internal void OnDialogFinish()
    {
        nodeIndex = 0;
        speaking = false;
        SceneManager.Instance.DisableAllColliders(speaking, this.gameObject);
        EventManager.Instance.TriggerEvent(currentDialog.dialogEventIndex);
    }

    public void OnDialogNodeFinish(int index)
    {
        if (currentNode.toolID != 0)
        {
            ToolManager.Instance.GiveTool(currentNode.toolID);
        }

        if (currentNode.npcID != 0)
        {
            CharInfoManager.Instance.MeetNPC(currentNode.npcID);
        }

        nodeIndex = index;
        currentNode = currentDialog.dialogNodes[nodeIndex];
    }
}
