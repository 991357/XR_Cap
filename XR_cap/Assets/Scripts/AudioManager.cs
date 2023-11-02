using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM")]
    public AudioClip AC_Bgm;
    public float F_BgmVolume;
    AudioSource AS_Bgm;
    AudioHighPassFilter AF_BgmEffect;
    
    [Header("SFX")]
    public AudioClip[] AC_Sfx;
    public float F_SfxVolume;
    public int I_Channels;
    int I_ChannelIndex;
    AudioSource[ ] AS_Sfx;

    public enum Sfx { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win }


    private void Awake()
    {
        Instance = this;

        Init();

        //Bgm V - 0.2;
        //Sfx V - 0.5;
    }

    public void Init()
    {
        //배경음 플레이어 초기화
        GameObject bgm = new GameObject("Bgm");
        bgm.transform.parent = transform;
        AS_Bgm = bgm.AddComponent<AudioSource>();
        AS_Bgm.playOnAwake = false;
        AS_Bgm.loop = true;
        AS_Bgm.volume = F_BgmVolume;
        AS_Bgm.clip = AC_Bgm;
        AF_BgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        //효과음 플레이어 초기화
        GameObject sfx = new GameObject("Sfx");
        sfx.transform.parent = transform;
        AS_Sfx = new AudioSource[I_Channels];

        for(int i = 0; i <I_Channels; i++)
        {
            AS_Sfx[i] = sfx.AddComponent<AudioSource>();
            AS_Sfx[i].playOnAwake = false;
            AS_Sfx[i].bypassListenerEffects = true;
            AS_Sfx[i].volume = F_SfxVolume;
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for(int i = 0; i < I_Channels; i++)
        {
            int loopindex = (i + I_ChannelIndex) % I_Channels;

            if (AS_Sfx[loopindex].isPlaying)
                continue;

            int ran = 0;

            if(sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                ran = Random.Range(0, 2);
            }

            I_ChannelIndex = loopindex;
            AS_Sfx[loopindex].clip = AC_Sfx[(int)sfx];
            AS_Sfx[loopindex].Play();
            break;
        }
    }

    public void PlayBgm(bool isplay)
    {
        if(isplay)
        {
            AS_Bgm.Play();
        }
        else
        {
            AS_Bgm.Stop();
        }
    }

    public void EffectBgm(bool isplay)
    {
        AF_BgmEffect.enabled = isplay;
    }
}
