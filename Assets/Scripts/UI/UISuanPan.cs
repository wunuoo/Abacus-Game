using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISuanPan : MonoSingleton<UISuanPan>
{
    public Text suanPanValue;

    
    public void OnClickBack()
    {
        SceneManager.Instance.LoadScene("Main");
    }
    // Start is called before the first frame update

    public void SetValue(int value)
    {
        this.suanPanValue.text = value.ToString();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
