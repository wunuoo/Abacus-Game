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
        if (uiInstances.TryGetValue(typeof(T), out go)) //˵��UIʵ������
        {
            Debug.Log("find prefabToHide");
            go.SetActive(true); // ����ʵ������Ԥ����
        }
        else
        {
            go = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + fileName)); // �ڵ�ǰλ��ʵ����Ԥ����
            if (go != null)
            {
                uiInstances.Add(typeof(T), go);
            }
            else Debug.LogError("�Ҳ����ļ���" + "UI/" + fileName);
        }

        return go;
    }

    public void Hide(Type type)
    {
        GameObject go;
        if(uiInstances.TryGetValue(type, out go)) //˵��UIʵ������
        {
            Debug.Log("find prefabToHide");
            go.SetActive(false); // ����ʵ������Ԥ����
        }
        else
        {
            Debug.Log("can't find prefabToHide");
        }
 
    }

    internal void DeleteInstance<T>()//��ui�رյ�ʱ��Ӧ��ע��
    {
        GameObject ui;
        if(uiInstances.TryGetValue(typeof(T), out ui))
        {
            uiInstances.Remove(typeof(T));
        }

    }
}
