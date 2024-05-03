using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UIBase
{

    public Slider musicVolume;
    public ExtendedSlider soundVolume;

    private void Start()
    {
        soundVolume.value = SoundManager.Instance.SoundVolume;
        musicVolume.value = SoundManager.Instance.MusicVolume;
        //Debug.Log("addlistener");
        soundVolume.DragEnd.AddListener(this.OnSoundChangeEnd);
    }

    public void OnMusicVChange(float newValue)
    {
        SoundManager.Instance.OnChangeVolume(SoundType.Music, newValue);
    }

    public void OnSoundChangeEnd()//这个事件绑定在ExtendSlider组件上
    {
        SoundManager.Instance.PlaySound("exp_sound");
    }
    public void OnSoundVChange(float newValue)
    {
        SoundManager.Instance.OnChangeVolume(SoundType.Sound, newValue);        
    }

}
