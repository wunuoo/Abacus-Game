using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "创建对话", fileName = "对话")]
public class Dialog : ScriptableObject
{
    public List<DialogNode> dialogNodes = new List<DialogNode>();

    public int dialogEventIndex;//此段对话完成后，将会触发何种事件，事件表详见EventManager
}


