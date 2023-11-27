using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameObject FadeCanvas;

    public void OnClickGameStartFade()
    {
        FadeCanvas.SetActive(true);
        FadeCanvas.GetComponent<Animator>().SetTrigger("In");
    }
}
