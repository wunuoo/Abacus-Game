using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRecord : UIBase
{
    public TextMeshProUGUI[] records;

    private void Start()
    {
        RecordManager.Instance.OnRecordChange.AddListener(Refresh);
        Refresh();
    }

    public void Refresh()
    {
        for(int i = -1; i < RecordManager.Instance.recordsUnlockIndex; i++)
        {
            records[i + 1].gameObject.SetActive(true);
        }
    }
}
