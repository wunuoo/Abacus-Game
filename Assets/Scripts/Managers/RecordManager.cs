using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecordManager : Singleton<RecordManager>
{
    public int recordsUnlockIndex = -1;

    public UnityEvent OnRecordChange = new UnityEvent();

    public void GetRecord()
    {
        recordsUnlockIndex++;
        OnRecordChange?.Invoke();
    }

}
