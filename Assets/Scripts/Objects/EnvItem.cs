using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvItem : MonoBehaviour
{
    public int ID;

    public UIEnvInfo owner;
    public void OnMouseDown()
    {
        owner.OnClickEnvItem(ID);
    }

}
