using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseGame : UIBase
{
    public void OnClickBackToTitle()
    {
        Destroy(UIMain.Instance.gameObject);
        Destroy(UIManager.Instance.GetElementByType<UIDialog>());
        Destroy(DialogManager.Instance.gameObject);
        Destroy(ToolManager.Instance.gameObject);
        Destroy(ChapterManager.Instance.gameObject);
        SceneManager.Instance.LoadScene("Title");
    }

    public void OnClickSetting()
    {
        UIManager.Instance.Show<UISetting>();
    }

    public void OnClickExit()
    {
        //ÍË³öÓÎÏ·
        Application.Quit();
    }
}
