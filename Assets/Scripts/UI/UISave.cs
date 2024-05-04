using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISave : UIBase
{
    public List<UISaveData> uis;

    // Start is called before the first frame update
    void Start()
    {
        SaveManager.Instance.OnGameSave += this.Refresh;
        Refresh();
        base.Start();
    }

    void Refresh()
    {
        for (int i = 0; i < SaveManager.Instance.saves.Length; i++)
        {
            Save save = SaveManager.Instance.saves[i];
            uis[i].SetSaveInfo(save);
        }
    }

    public void OnClickBack()
    {
        this.OnClose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected new void OnDestroy()
    {
        SaveManager.Instance.OnGameSave -= this.Refresh;
    }
}
