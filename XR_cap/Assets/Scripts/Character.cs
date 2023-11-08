using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //public static float Speed
    //{
    //    get { return GameManager.Instance.I_PlayerId == 0 ? 1.1f : 1f; }
    //}

    public static float WeaponSpeed  //Character 3�� ����Ǿ�����   //����� 10% ����
    {
        get { return GameManager.Instance.PlayerId == 3 ? 1.1f : 1f; }
    }

    public static float WeaponRate      //Character 1�� ����Ǿ�����   //����� 10% ����
    {
        get { return GameManager.Instance.PlayerId == 1 ? 0.9f : 1f; }
    }
    public static float WeaponDmg       //Character 0�� ����Ǿ�����   //���⵩ 20% ����
    {
        get { return GameManager.Instance.PlayerId == 0 ? 1.2f : 1f; }
    }
    public static int WeaponCount       //CHaracter 2�� ����Ǿ� ����  //���� ���� 1�� �߰�
    {
        get { return GameManager.Instance.PlayerId == 2 ? 2 : 0; }
    }
}
