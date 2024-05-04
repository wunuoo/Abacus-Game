using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvItem : MonoBehaviour
{
    public int ID;

    public UIEnvInfo owner;

    public GameObject shiningPic;//�ڵ�һ�ε��֮ǰ������ᷢ��
    public GameObject normalPic;//�ڵ�һ�ε��֮ǰ������ᷢ��
    public void OnMouseDown()
    {
        if (shiningPic.activeSelf)
        {
            shiningPic.SetActive(false);
            normalPic.SetActive(true);
        }
        owner.OnClickEnvItem(ID);
    }

}
