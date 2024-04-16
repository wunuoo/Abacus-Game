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
    public Dialog[] dialogs;

    public void PlayDialog(int index)
    {

        UIDialog ui = UIManager.Instance.Show<UIDialog>("UIDialog").GetComponent<UIDialog>();
        ui.StartDialog(dialogs[index]);
    }


}
