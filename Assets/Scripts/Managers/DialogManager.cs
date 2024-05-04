using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogNode
{
    [Header("说话者")]
    public string name;//这个是给人看的，代码直接读取ID
    [Header("说话者ID")]
    public int ID;
    [Header("肖像")]
    public Sprite portrait;//这个是给人看的，代码直接读取ID，也提供了手动微调的空间
    [TextArea, Header("说话内容")]
    public string content;
    [Header("道具ID")]
    public int toolID = 0;//如果是0，不显示道具，否则显示对应道具
    [Header("NPC信息")]
    public int npcID = 0;//如果是0，不反应，否则获取对应NPC信息
}

public class DialogManager : MonoSingleton<DialogManager>
{
    bool speaking = false;

    Dialog currentDialog;

    public DialogNode currentNode;
    public int nodeIndex;

    //处理从ChapterManager传来的指令
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

    //当用户点击最后一个下一句按钮后，触发这个函数
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
