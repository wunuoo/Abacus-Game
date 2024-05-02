using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnd : MonoBehaviour
{
    public void OnClickBackToTitle()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        Destroy(UIMain.Instance.gameObject);
        Destroy(UIManager.Instance.GetElementByType<UIDialog>());
        Destroy(DialogManager.Instance.gameObject);
        Destroy(ToolManager.Instance.gameObject);
        Destroy(ChapterManager.Instance.gameObject);
        Destroy(CGManager.Instance.gameObject);
        SceneManager.Instance.LoadScene("Title");
    }
}
