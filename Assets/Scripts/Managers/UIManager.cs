using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject prefabToSpawn; // 实例化的预制体
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
            prefabToSpawn = Resources.Load<GameObject>("UI/UISetting"); // 加载预制体资源
            prefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity); // 在当前位置实例化预制体
        }
        else
        {
            prefab.SetActive(true); // 显示实例化的预制体
        }
    }

}
