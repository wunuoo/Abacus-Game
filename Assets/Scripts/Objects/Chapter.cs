using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "�����½�", fileName = "�½�")]
public class Chapter : ScriptableObject
{
    public string chapterName;

    public Dialog[] dialogs;

    public Task[] tasks;

    public Sprite envBG;

    public bool inShop;

    public bool hasOpenCG;

    //ÿ����ȡһ���¶Ի��������񣬾ͻ��Զ���ָ��+1����ע��
}
