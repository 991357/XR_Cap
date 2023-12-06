using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;

    [Header("SFX")]
    public AudioClip[] AC_Sfx;
    public float F_SfxVolume;
    public int I_Channels;
    int I_ChannelIndex;
    AudioSource[] AS_Sfx;

    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win, Click, UltimateFilled }

    private void Awake()
    {
        Instance = this;

        Init();

        //Bgm V - 0.2;
        //Sfx V - 0.5;
    }

    public void Init()
    {
        //효과음 플레이어 초기화
        GameObject sfx = new GameObject("Sfx");
        sfx.transform.parent = transform;
        AS_Sfx = new AudioSource[I_Channels];

        for (int i = 0; i < I_Channels; i++)
        {
            AS_Sfx[i] = sfx.AddComponent<AudioSource>();
            AS_Sfx[i].playOnAwake = false;
            AS_Sfx[i].bypassListenerEffects = true;
            AS_Sfx[i].volume = F_SfxVolume;
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < I_Channels; i++)
        {
            int loopindex = (i + I_ChannelIndex) % I_Channels;

            if (AS_Sfx[loopindex].isPlaying)
                continue;

            I_ChannelIndex = loopindex;
            AS_Sfx[loopindex].clip = AC_Sfx[(int)sfx];
            AS_Sfx[loopindex].Play();
            break;
        }
    }
}
