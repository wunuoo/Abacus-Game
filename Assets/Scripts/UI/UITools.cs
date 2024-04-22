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


    public void ShowDescription(int index)
    {
        ToolInfo toolInfo = ToolInfoManager.Instance.tools[index];
        toolName.text = toolInfo.toolName;
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
