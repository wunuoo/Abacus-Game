using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoSingleton<SceneManager>
{
    UnityAction<float> onProgress = null;
    public UnityEvent loadCompleted = new UnityEvent();

    public float fadeSpeed = 3f;
    public RawImage muskImage;

    string lastSceneName = "Title";
    private bool fadeOutMode;

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
        //拉上黑幕
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
        loadCompleted.RemoveAllListeners();//这个事件是一次性的
        
    }

}
