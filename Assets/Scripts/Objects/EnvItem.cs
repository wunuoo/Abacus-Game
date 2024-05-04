using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvItem : MonoBehaviour
{
    public int ID;

    public UIEnvInfo owner;

    public GameObject shiningPic;//在第一次点击之前，物体会发光
    public GameObject normalPic;//在第一次点击之前，物体会发光

    private void Start()
    {
        shiningPic.SetActive(ChapterManager.Instance.envItemShining[ID]);
        normalPic.SetActive(!ChapterManager.Instance.envItemShining[ID]);
    }

    public void OnMouseDown()
    {
        if (shiningPic.activeSelf)
        {
            shiningPic.SetActive(false);
            normalPic.SetActive(true);
            ChapterManager.Instance.envItemShining[ID] = false;
        }
        owner.OnClickEnvItem(ID);
    }

}
