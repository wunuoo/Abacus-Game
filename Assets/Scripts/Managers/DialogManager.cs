using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogNode
{
    [Header("˵����")]
    public string name;
    [Header("Ф��")]
    public Sprite portrait;
    [TextArea, Header("˵������")]
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
