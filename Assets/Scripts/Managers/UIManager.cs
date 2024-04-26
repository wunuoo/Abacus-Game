using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{

    public virtual void OnClose()
    {
        Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
        //UIManager.Instance.DeleteInstance(this.gameObject);
    }
}



public class UIManager : Singleton<UIManager>
{
    class UIElement
    {
        public string uiName;
        public GameObject instance;
    }

    public Dictionary<Type, string> uiNames = new Dictionary<Type, string>();

    Dictionary<Type, UIElement> uis = new Dictionary<Type, UIElement>();

    //public Dictionary<Type, string> uiNames = new Dictionary<Type, string>();

    // Start is called before the first frame update
    public UIManager()
    {
        uis.Add(typeof(UIDialog), new UIElement() { uiName = "UIDialog"});
        uis.Add(typeof(UISetting), new UIElement() { uiName = "UISetting" });
        uis.Add(typeof(UITools), new UIElement() { uiName = "UITools" });
        uis.Add(typeof(UIPauseGame), new UIElement() { uiName = "UIPauseGame" });
        uis.Add(typeof(UIRecord), new UIElement() { uiName = "UIRecord" });
    }

    public T Show<T>()
    {
        UIElement ui;
        if (uis.TryGetValue(typeof(T), out ui)) 
        {
            Debug.Log("find prefabHidden");

            if(ui.instance != null)
                ui.instance.SetActive(true); // ��ʾ�Ѿ�ʵ�����������ص�Ԥ����
            else
            {
                string name = "UI/" + ui.uiName;
                GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(name)); // �ڵ�ǰλ��ʵ����Ԥ����
                ui.instance = go;
            }
        }
        else Debug.LogError("�Ҳ����ļ���" + typeof(T).ToString());
    
        return ui.instance.GetComponent<T>();
    }

    public GameObject GetElementByType<T>()
    {
        GameObject instance = null;
        if (uis.ContainsKey(typeof(T)))
        {
            instance = uis[typeof(T)].instance;
        }
        else
        {
            Debug.LogError("��Ϸui���󲻴��ڣ�" + typeof(T).ToString());
        }
        return instance;
    }

    //public void Hide(Type type)
    //{
    //    GameObject go;
    //    if(uiInstances.TryGetValue(type, out go)) //˵��UIʵ������
    //    {
    //        Debug.Log("find prefabToHide");
    //        go.SetActive(false); // ����ʵ������Ԥ����
    //    }
    //    else
    //    {
    //        Debug.Log("can't find prefabToHide");
    //    }
 
    //}

    //internal void DeleteInstance(GameObject obj)//��ui�رյ�ʱ��Ӧ��ע��
    //{
    //    GameObject ui;
    //    if(uiInstances.TryGetValue(obj.GetType(), out ui))
    //    {
    //        uiInstances.Remove(obj.GetType());
    //    }

    //}
}
