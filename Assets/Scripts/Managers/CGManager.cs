using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class CGManager : MonoSingleton<CGManager>
{
    public Image chapterMusk;
    public Slider chapterPointer;
    public TextMeshProUGUI chapterTitle;

    //这几个是ppt相关的
    public Image BGPic;
    public RawImage blackMusk;
    public float fadeSpeed = 3f;
    public GameObject PPTDisplay;
    public Sprite[] pics;
    public int pptIndex = 0;

    public PlayableAsset black_Timeline;

    public PlayableDirector director;

    

    public Chapter testchapter;

    public UnityEvent OnBlackMuskFaded = new UnityEvent();


    public void PlayChapterBegin(Chapter chapter, int index)
    {
        director.playableAsset = black_Timeline;
        chapterTitle.text = chapter.chapterName;
        chapterPointer.value = index;
        chapterMusk.gameObject.SetActive(true);
        director.stopped += OnAnimStopped;
        director.Play();
        
    }

    private void OnAnimStopped(PlayableDirector obj)
    {
        OnBlackMuskFaded?.Invoke();
        OnBlackMuskFaded?.RemoveAllListeners();
    }

    protected override void OnStart()
    {
        chapterPointer.maxValue = ChapterManager.Instance.chapters.Count - 1;
        //PlayChapterBegin(testchapter, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void PlayBegin()
    {
        //beginCG.gameObject.SetActive(true);
        //director.stopped += OnBeginAnimStop;
        //director.Play();
    }

    public void ShowNextImage()
    {
        
        PPTDisplay.SetActive(true);
        StartCoroutine(GameUtil.FadeIn(blackMusk, fadeSpeed, () =>
        {
            
            this.BGPic.sprite = pics[pptIndex];
            pptIndex++;
            this.BGPic.gameObject.SetActive(true);
            StartCoroutine(GameUtil.FadeOut(blackMusk, fadeSpeed));
        }));
        
    }

    public void HidePPT()
    {
        StartCoroutine(GameUtil.FadeIn(blackMusk, fadeSpeed, () =>
        {
            this.BGPic.gameObject.SetActive(false);
            StartCoroutine(GameUtil.FadeOut(blackMusk, fadeSpeed, () => { PPTDisplay.SetActive(false); }));
        }));
        
        
    }
}
