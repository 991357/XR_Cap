using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType Type;
    public float F_Rate;

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Gear" + data.I_ItemId;
        transform.parent = GameManager.Instance.Player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        Type = data.Type;
        F_Rate = data.F_Dmgs[0];
        ApplyGear();
    }    

    void ApplyGear()
    {
        switch(Type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                Debug.Log("삽 회전 속도 증가");
                break;
            case ItemData.ItemType.Shoe:
                SpeedUP();
                Debug.Log("이동속도 증가");
                break;
            case ItemData.ItemType.GunRateUp:
                GunRateUp();
                Debug.Log("총알 연사속도 증가");
                break;
        }
    }

    public void LevelUP(float rate)
    {
        F_Rate = rate;
        ApplyGear();
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch (weapon.I_Id)
            {
                case 0:
                    float speed = 150 * Character.WeaponSpeed;
                    weapon.F_Speed = speed + (150 * F_Rate);
                    break;
                //case 1:
                //    speed = 0.5f * Character.WeaponRate;
                //    weapon.F_Speed = speed * (1f - F_Rate);
                //    break;
            }
        }
    }
      
    void SpeedUP()
    {
        //float speed = GameManager.Instance.S_Player.F_Speed * Character.Speed;
        float speed = GameManager.Instance.PlayerLogic.Speed;

        GameManager.Instance.PlayerLogic.Speed = speed + speed * F_Rate;
    }

    void GunRateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch (weapon.I_Id)
            {
                //case 0:
                //    float speed = 150 * Character.WeaponSpeed;
                //    weapon.F_Speed = speed + (150 * F_Rate);
                //    break;
                case 1:
                    float speed = 1f * Character.WeaponRate;
                    weapon.F_Speed = speed * (1f - F_Rate);
                    break;
            }
        }
    }
}
