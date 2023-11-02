using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform RT_Rect;

    private void Awake()
    {
        RT_Rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        RT_Rect.position = Camera.main.WorldToScreenPoint(GameManager.Instance.Player.transform.position+Vector3.up *0.5f);
    }
}