using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogNode
{
    [Header("说话者")]
    public string name;
    [Header("肖像")]
    public Sprite portrait;
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
