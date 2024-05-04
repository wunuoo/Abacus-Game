using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITools : UIBase
{
    public GameObject descriptionPanel;
    public TextMeshProUGUI toolName;
    public TextMeshProUGUI toolDescription;
    public Image toolImage;

    public GameObject[] toolButtons;

    private void Start()
    {
        ToolManager.Instance.OnToolChange.AddListener(this.Refresh);
        Refresh();
        base.Start();
    }

    void Refresh()
    {
        foreach(var kv in ToolManager.Instance.toolGotten)
        {
            toolButtons[kv.Key - 1].SetActive(kv.Value == true);//有点烂，但先这么写着

        }

    }


    public void ShowDescription(int index)
    {
        ToolInfo toolInfo = ToolManager.Instance.tools[index];
        toolName.text = "<rotate=90>" + toolInfo.toolName;
        toolDescription.text = "<rotate=90>" + toolInfo.toolDescription;
        toolImage.sprite = toolInfo.toolImage;
        toolImage.SetNativeSize();
        descriptionPanel.SetActive(true);
    }

    public void HideDescription()
    {
        descriptionPanel.SetActive(false);
    }

    public void OnClickTool(int index)
    {
        ShowDescription(index);

    }


}
