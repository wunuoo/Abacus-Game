using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SceneManager : MonoSingleton<SceneManager>
{
    UnityAction<float> onProgress = null;
    public UnityEvent loadCompleted = new UnityEvent();

    public float fadeSpeed = 3f;
    public RawImage muskImage;

    string lastSceneName = "Title";
    private bool fadeOutMode;

    public List<GameObject> uiLockItem = new List<GameObject>();

    // Use this for initialization
    protected override void OnStart()
    {

    }

    public bool IsOnTitle()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Title";
    }
    public bool IsOnMain()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main";
    }

    public void BackToLastScene()
    {
        LoadScene(lastSceneName);
    }

    public void LoadScene(string name, bool fadeout = true)
    {
        lastSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        this.fadeOutMode = fadeout;
        StartCoroutine(LoadLevel(name));
        //LoadLevelQuick(name);
    }

    void LoadLevelQuick(string name)
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    IEnumerator LoadLevel(string name)
    {
        //���Ϻ�Ļ
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            muskImage.color = new Color(0, 0, 0, alpha);
            yield return null;

        }


        Debug.LogFormat("LoadLevel: {0}", name);
        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);
        async.allowSceneActivation = true;
        async.completed += LevelLoadCompleted;
        while (!async.isDone)
        {
            if (onProgress != null)
                onProgress(async.progress);
            yield return null;
        }


    }

    private void LevelLoadCompleted(AsyncOperation obj)
    {
        if (onProgress != null)
            onProgress(1f);
        Debug.Log("LevelLoadCompleted:" + obj.progress);

        if (fadeOutMode)
            StartCoroutine(GameUtil.FadeOut(muskImage, fadeSpeed));
        else
            muskImage.color = new Color(0, 0, 0, 0);

        loadCompleted?.Invoke();
        loadCompleted.RemoveAllListeners();//����¼���һ���Ե�
        
    }

    // ���û������������������ϵ�Collider���,ע��false������
    public void DisableAllColliders(bool add, GameObject uiObject)
    {
        Debug.Log("������ײ�䣺" + !add + "   ���⣺ " + uiObject.name);
        // ��ȡ���������е�Collider���
        Collider[] colliders = FindObjectsOfType<Collider>();
        Collider2D[] collider2ds = FindObjectsOfType<Collider2D>();

        if (add)
        {
            uiLockItem.Add(uiObject);
        }
        else
        {
            uiLockItem.Remove(uiObject);
        }

        bool lockSet = uiLockItem.Count != 0;//ֻҪList����һ�����壬����ס����

        // ѭ����������Collider���
        foreach (Collider collider in colliders)
        {
            // �ų�UI���������Collider���
            //if (collider.gameObject != uiObject)
            //{
            //    // ����Collider������Ƿ�����״̬
            //    collider.enabled = !add;
            //}
            collider.enabled = !lockSet;
        }

        // ѭ����������Collider2D���
        foreach (Collider2D collider in collider2ds)
        {
            // �ų�UI���������Collider���
            //if (collider.gameObject != uiObject)
            //{
            //    // ����Collider������Ƿ�����״̬
            //    collider.enabled = !add;
            //}
            collider.enabled = !lockSet;

        }
    }
}
