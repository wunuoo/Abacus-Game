using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayMusic("bgm_title");

    }

    public void OnClickStart()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        SceneManager.Instance.LoadScene("Main");
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);
        //ÍË³öÓÎÏ·
        Application.Quit();
    }

    public void OnClickSetting()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        UIManager.Instance.Show<UISetting>();
    }

    public void OnClickSave()
    {
        SoundManager.Instance.PlaySound(GameConfig.ButtonSound);

        UIManager.Instance.Show<UISave>();
    }
}
