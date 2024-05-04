using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnvInfo : MonoBehaviour
{
    public UIEnvInfoBar[] info_bars;

    internal void OnClickEnvItem(int index)
    {
        for (int i = 0; i < info_bars.Length; i++)
        {
            UIEnvInfoBar ui = info_bars[i];
            if (i == index)
            {
                ui.showed = !ui.showed;
            }
            else ui.showed = false;

            ui.anim.SetBool("Show", ui.showed);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
