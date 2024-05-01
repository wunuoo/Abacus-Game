using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class GameUtil
{
    public static void HightLight(Image img)
    {
        img.color = Color.white;
    }

    public static void LowLight(Image img)
    {
        img.color = Color.gray;

    }
}
