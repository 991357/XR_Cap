using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool B_IsLeft;
    public SpriteRenderer SR_Sprite;

    SpriteRenderer SR_Player;

    Vector3 V_RightPos = new Vector3(0.3f,-0.13f,0);
    Vector3 V_RightPosFlip = new Vector3(-0.2f,-0.13f,0);
    Quaternion Q_LeftRot = Quaternion.Euler(0, 0, -35);
    Quaternion Q_LeftRotFlip = Quaternion.Euler(0, 0, -135);
    private void Awake()
    {
        SR_Player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isflip = SR_Player.flipX;

        if(B_IsLeft)        //근접무기 
        {
            transform.localRotation = isflip ? Q_LeftRotFlip : Q_LeftRot;
            SR_Sprite.flipY = isflip;
            SR_Sprite.sortingOrder = isflip ? 4 : 6;
        }
        else                //원거리무기
        {
            transform.localPosition = isflip ? V_RightPosFlip : V_RightPos;
            SR_Sprite.flipX = isflip;
            SR_Sprite.sortingOrder = isflip ? 6 : 4;
        }
    }
}
