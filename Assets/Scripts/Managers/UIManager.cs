using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject prefabToSpawn; // ʵ������Ԥ����
    public static GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSetting()
    {
        
        if (prefabToSpawn == null)
        {
            prefabToSpawn = Resources.Load<GameObject>("UI/UISetting"); // ����Ԥ������Դ
            prefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity); // �ڵ�ǰλ��ʵ����Ԥ����
        }
        else
        {
            prefab.SetActive(true); // ��ʾʵ������Ԥ����
        }
    }

}
