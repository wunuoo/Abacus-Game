using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "��������", fileName = "����")]
public class Task : ScriptableObject
{
    [Header("����ID")]
    public int taskID;

    [Header("�����м���")]
    public int[] results;//�м���

    [Header("���񴥷��¼�")]
    public int eventIndex = 2;//һ������´���2���¼��������Ի�

    [TextArea, Header("�����")]
    public string report;//�����
}
