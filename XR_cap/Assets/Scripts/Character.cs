using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //public static float Speed
    //{
    //    get { return GameManager.Instance.I_PlayerId == 0 ? 1.1f : 1f; }
    //}

    public static float WeaponSpeed  //Character 3에 적용되어있음   //무기속 10% 증가
    {
        get { return GameManager.Instance.PlayerId == 3 ? 1.1f : 1f; }
    }

    public static float WeaponRate      //Character 1에 적용되어있음   //무기속 10% 증가
    {
        get { return GameManager.Instance.PlayerId == 1 ? 0.9f : 1f; }
    }
    public static float WeaponDmg       //Character 0에 적용되어있음   //무기뎀 20% 증가
    {
        get { return GameManager.Instance.PlayerId == 0 ? 1.2f : 1f; }
    }
    public static int WeaponCount       //CHaracter 2에 적용되어 있음  //무기 개수 1개 추가
    {
        get { return GameManager.Instance.PlayerId == 2 ? 2 : 0; }
    }
}
