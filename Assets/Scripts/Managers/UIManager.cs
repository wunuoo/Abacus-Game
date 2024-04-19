using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public Dictionary<Type, string> uiNames = new Dictionary<Type, string>();

    public Dictionary<Type, GameObject> uiInstances = new Dictionary<Type, GameObject>();

    //public Dictionary<Type, string> uiNames = new Dictionary<Type, string>();

    // Start is called before the first frame update
    public UIManager()
    {
        uiNames.Add(typeof(UIDialog), "UIDialog");
        uiNames.Add(typeof(UISetting), "UISetting");
    }

    public T Show<T>()
    {
        GameObject go;
        if (uiInstances.TryGetValue(typeof(T), out go)) //˵��UIʵ������
        {
            Debug.Log("find prefabHidden");
            go.SetActive(true); // ��ʾ�Ѿ�ʵ�����������ص�Ԥ����
        }
        else
        {
            string name = "UI/" + uiNames[typeof(T)];
            go = GameObject.Instantiate(Resources.Load<GameObject>(name)); // �ڵ�ǰλ��ʵ����Ԥ����
            if (go != null)
            {
                uiInstances.Add(typeof(T), go);
            }
            else Debug.LogError("�Ҳ����ļ���" + name);
        }

        return go.GetComponent<T>();
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
