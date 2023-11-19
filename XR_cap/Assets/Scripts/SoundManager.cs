using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource Bgm;

    public void SetMusicVolume(float Value)
    {
        Bgm.volume = Value;
    }
}
