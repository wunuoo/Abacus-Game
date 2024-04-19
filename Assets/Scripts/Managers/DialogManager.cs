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
}

public class DialogManager : MonoSingleton<DialogManager>
{
    bool speaking = false;

    Dialog currentDialog;

    //处理从ChapterManager传来的指令
    public void PlayDialog(Dialog dialog)
    {
        UIDialog ui = UIManager.Instance.Show<UIDialog>();
        ui.StartDialog(dialog);

        currentDialog = dialog;
        speaking = true;
        DisableAllColliders(speaking);
    }

    //当用户点击最后一个下一句按钮后，触发这个函数
    internal void OnDialogFinish()
    {
        speaking = false;
        DisableAllColliders(speaking);
        EventManager.Instance.TriggerEvent(currentDialog.dialogEventIndex);
    }

    // 禁用或启用所有其他对象上的Collider组件
    private void DisableAllColliders(bool disable)
    {
        // 获取场景中所有的Collider组件
        Collider[] colliders = FindObjectsOfType<Collider>();
        Collider2D[] collider2ds = FindObjectsOfType<Collider2D>();

        // 循环遍历所有Collider组件
        foreach (Collider collider in colliders)
        {
            // 排除UI对象自身的Collider组件
            if (collider.gameObject != gameObject)
            {
                // 设置Collider组件的是否启用状态
                collider.enabled = !disable;
            }
        }

        // 循环遍历所有Collider2D组件
        foreach (Collider2D collider in collider2ds)
        {
            // 排除UI对象自身的Collider组件
            if (collider.gameObject != gameObject)
            {
                // 设置Collider组件的是否启用状态
                collider.enabled = !disable;
            }
        }
    }


}
