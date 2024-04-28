using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoSingleton<SceneManager>
{
    UnityAction<float> onProgress = null;
    public UnityAction loadCompleted = null;

    public float fadeSpeed = 3f;
    public RawImage muskImage;

    // Use this for initialization
    protected override void OnStart()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadLevel(name));
        //LoadLevelQuick(name);
    }

    void LoadLevelQuick(string name)
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    IEnumerator LoadLevel(string name)
    {
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

        StartCoroutine(FadeOut());
        if(loadCompleted != null)
        {
            this.loadCompleted();
        }
        
    }

    IEnumerator FadeOut()
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            muskImage.color = new Color(0, 0, 0, alpha);
            yield return null;

        }
    }
}
