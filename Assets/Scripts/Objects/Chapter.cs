using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "�����½�", fileName = "�½�")]
public class Chapter : ScriptableObject
{
    public string chapterName;

    public Dialog[] dialogs;

    public Task[] tasks;

    //ÿ����ȡһ���¶Ի��������񣬾ͻ��Զ���ָ��+1����ע��
}
