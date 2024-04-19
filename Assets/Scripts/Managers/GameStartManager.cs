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
        SceneManager.Instance.LoadScene("Main");
    }

    public void OnClickExit()
    {

    }

    public void OnClickSetting()
    {
        UIManager.Instance.Show<UISetting>();
    }
}
