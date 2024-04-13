using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    
    

    public Dictionary<Type, GameObject> uiInstances = new Dictionary<Type, GameObject>();

    //public Dictionary<Type, string> uiNames = new Dictionary<Type, string>();

    // Start is called before the first frame update
    public UIManager()
    {

    }

    public GameObject Show<T>(string fileName)
    {
        GameObject go;
        if (uiInstances.TryGetValue(typeof(T), out go)) //说明UI实例存在
        {
            Debug.Log("find prefabToHide");
            go.SetActive(true); // 隐藏实例化的预制体
        }
        else
        {
            go = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + fileName)); // 在当前位置实例化预制体
            if (go != null)
            {
                uiInstances.Add(typeof(T), go);
            }
            else Debug.LogError("找不到文件：" + "UI/" + fileName);
        }

        return go;
    }

    public void Hide(Type type)
    {
        GameObject go;
        if(uiInstances.TryGetValue(type, out go)) //说明UI实例存在
        {
            Debug.Log("find prefabToHide");
            go.SetActive(false); // 隐藏实例化的预制体
        }
        else
        {
            Debug.Log("can't find prefabToHide");
        }
 
    }

    internal void DeleteInstance<T>()//当ui关闭的时候应该注销
    {
        GameObject ui;
        if(uiInstances.TryGetValue(typeof(T), out ui))
        {
            uiInstances.Remove(typeof(T));
        }

    }
}
