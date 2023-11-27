using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DotTest : MonoBehaviour
{
    private void Start()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.InExpo));
        //.Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f).SetEase(Ease.InQuart))
        //.Append(transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce));
    }
}
