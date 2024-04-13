using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{

    public Slider musicVolume;
    public ExtendedSlider soundVolume;

    private void Start()
    {
        
        Debug.Log("addlistener");
        soundVolume.DragEnd.AddListener(this.OnSoundChangeEnd);
    }

    public void OnMusicVChange(float newValue)
    {
        SoundManager.Instance.OnChangeVolume(SoundType.Music, newValue);
    }

    public void OnSoundChangeEnd()//����¼�����ExtendSlider�����
    {
        SoundManager.Instance.PlaySound("exp_sound");
    }
    public void OnSoundVChange(float newValue)
    {
        SoundManager.Instance.OnChangeVolume(SoundType.Sound, newValue);        
    }

    public void Hide()
    {
        UIManager.Instance.Hide(GetType());
    }

    public void OnDestroy()
    {
        UIManager.Instance.DeleteInstance<UISetting>();
    }
}
