using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "��������", fileName = "����")]
public class Task : ScriptableObject
{
    public int taskID;
    public int[] results;//�м���
    public int nowIndex = 0;//Ŀǰ�������м������

}
