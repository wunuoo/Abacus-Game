using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UISetting : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HideSetting()
    {
        if (UIManager.prefab != null)
        {
            Debug.Log("find prefabToHide");
            UIManager.prefab.SetActive(false); // 隐藏实例化的预制体
        }
        else
        {
            Debug.Log("can't find prefabToHide");
        }
    }
}
