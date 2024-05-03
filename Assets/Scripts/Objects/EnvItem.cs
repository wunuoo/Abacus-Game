using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvItem : MonoBehaviour
{
    public int ID;

    public void OnMouseDown()
    {
        UIMain.Instance.OnClickEnvItem(ID);
    }

}
