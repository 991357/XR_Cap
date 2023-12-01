using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEvent : MonoBehaviour
{
    public GameObject FadeOut;

    public void OnClickFadeOutStartBtn()
    {
        FadeOut.SetActive(true);
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        FadeOut.GetComponent<Animator>().SetTrigger("Out");
    }

    public void FadeOutOff()
    {
        FadeOut.SetActive(false);
    }
}
