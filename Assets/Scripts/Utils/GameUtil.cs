using System;
using System.Collections;
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

    public static IEnumerator FadeOut(RawImage muskImage, float fadeSpeed, Action recallFunc = null)
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            muskImage.color = new Color(0, 0, 0, alpha);
            yield return null;

        }
        recallFunc?.Invoke();
    }

    public static IEnumerator FadeIn(RawImage muskImage, float fadeSpeed, Action recallFunc = null)
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            muskImage.color = new Color(0, 0, 0, alpha);
            yield return null;

        }
        recallFunc?.Invoke();
    }


}
