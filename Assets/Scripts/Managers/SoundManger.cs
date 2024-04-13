using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Sound,
    Music
}

public class SoundManager : MonoSingleton<SoundManager>
{
    float soundVolume;
    float musicVolume;

    public AudioSource BGMPlayer;
    public AudioSource SoundPlayer;

    string soundPath = "Sound/";
    string musicPath = "Music/";

    public void OnChangeVolume(SoundType type, float value)
    {
        switch (type)
        {
            case SoundType.Sound:
                SoundPlayer.volume = value / 100f;
                break;
            case SoundType.Music:
                BGMPlayer.volume = value / 100f;
                
                break;
            default:
                break;
        }
    }

    public void PlayMusic(string fileName)
    {
        AudioClip clip = Resources.Load<AudioClip>(musicPath + fileName);
        if(clip != null)
        {
            BGMPlayer.clip = clip;
            BGMPlayer.Play();
        }
        else
        {
            Debug.LogError("找不到音乐文件： " + musicPath + fileName);
        }
    }

    public void PlaySound(string fileName)
    {
        AudioClip clip = Resources.Load<AudioClip>(soundPath + fileName);
        if (clip != null)
        {
            SoundPlayer.clip = clip;
            SoundPlayer.Play();
        }
        else
        {
            Debug.LogError("找不到音效文件： " + soundPath + fileName);
        }
    }
}
