using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvItem : MonoBehaviour
{
    public int ID;

    public UIEnvInfo owner;

    public GameObject shiningPic;//�ڵ�һ�ε��֮ǰ������ᷢ��
    public GameObject normalPic;//�ڵ�һ�ε��֮ǰ������ᷢ��

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
