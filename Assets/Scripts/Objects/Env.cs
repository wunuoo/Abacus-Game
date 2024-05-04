using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//场景，主要是背景
public class Env : MonoBehaviour
{
    public SpriteRenderer envBG;

    public UIEnvInfo infoBar;

    public GameObject[] envItems;
    // Start is called before the first frame update
    void Start()
    {
        ChapterManager.Instance.OnLoadFinish.AddListener(this.Refresh);
        ChapterManager.Instance.OnNewChapterStart.AddListener(this.Refresh);
        Refresh();
    }
    private void OnDestroy()
    {
        ChapterManager.Instance.OnLoadFinish.RemoveListener(this.Refresh);
        ChapterManager.Instance.OnNewChapterStart.RemoveListener(this.Refresh);
    }

    void Refresh()
    {
        Chapter chapter = ChapterManager.Instance.currentChapter;
        if (chapter != null)
        {
            envBG.sprite = chapter.envBG;
            if (!chapter.inShop)
            {
                infoBar.gameObject.SetActive(false);
                foreach (var item in envItems)
                {
                    item.SetActive(false);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
