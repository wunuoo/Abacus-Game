using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "�����Ի�", fileName = "�Ի�")]
public class Dialog : ScriptableObject
{
    public List<DialogNode> dialogNodes = new List<DialogNode>();

    public int dialogEventIndex;//�˶ζԻ���ɺ󣬽��ᴥ�������¼����¼������EventManager
}


