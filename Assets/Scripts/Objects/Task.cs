using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "��������", fileName = "����")]
public class Task : ScriptableObject
{
    public int taskID;
    public int[] results;//�м���

    public int eventIndex = 2;//һ������´���2���¼��������Ի�
}
