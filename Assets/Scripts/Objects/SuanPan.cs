using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuanPan : MonoBehaviour
{

    int value;
    public int Value { get { return value; } }

    public void Add(int value)
    {
        this.value += value;
        UISuanPan.Instance.SetValue(this.value);

        TaskManager.Instance.CheckResult(this.value);
    }

}
