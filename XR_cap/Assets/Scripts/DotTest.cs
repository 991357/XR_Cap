using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DotTest : MonoBehaviour
{
    public string Type;

    public Image FadeIn;
    public Image FadeOut;
    private void Start()
    {
        switch (Type)
        {
            case "Success":
                Success();
                break;
            default:
                break;
        }
    }

    private void Update()
    {

    }
    public void Success()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.InExpo));
    }
}
