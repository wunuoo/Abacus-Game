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
}

public class DialogManager : MonoSingleton<DialogManager>
{
    bool speaking = false;

    Dialog currentDialog;

    //�����ChapterManager������ָ��
    public void PlayDialog(Dialog dialog)
    {
        UIDialog ui = UIManager.Instance.Show<UIDialog>();
        ui.StartDialog(dialog);

        currentDialog = dialog;
        speaking = true;
        DisableAllColliders(speaking);
    }

    //���û�������һ����һ�䰴ť�󣬴����������
    internal void OnDialogFinish()
    {
        speaking = false;
        DisableAllColliders(speaking);
        EventManager.Instance.TriggerEvent(currentDialog.dialogEventIndex);
    }

    // ���û������������������ϵ�Collider���
    private void DisableAllColliders(bool disable)
    {
        // ��ȡ���������е�Collider���
        Collider[] colliders = FindObjectsOfType<Collider>();
        Collider2D[] collider2ds = FindObjectsOfType<Collider2D>();

        // ѭ����������Collider���
        foreach (Collider collider in colliders)
        {
            // �ų�UI���������Collider���
            if (collider.gameObject != gameObject)
            {
                // ����Collider������Ƿ�����״̬
                collider.enabled = !disable;
            }
        }

        // ѭ����������Collider2D���
        foreach (Collider2D collider in collider2ds)
        {
            // �ų�UI���������Collider���
            if (collider.gameObject != gameObject)
            {
                // ����Collider������Ƿ�����״̬
                collider.enabled = !disable;
            }
        }
    }


}
