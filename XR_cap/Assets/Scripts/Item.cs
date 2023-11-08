
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData Data;
    public int Level;
    public Weapon Weapon;
    public Gear Gear;

    Image Img_Icon;
    Text T_Level;
    Text T_Name;
    Text T_Desc;

    private void Awake()
    {
        Img_Icon = GetComponentsInChildren<Image>()[1];
        Img_Icon.sprite = Data.SP_ItemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        T_Level = texts[0];
        T_Name = texts[1];
        T_Desc = texts[2];

        T_Name.text = Data.S_ItemName;
    }

    private void OnEnable()
    {
        T_Level.text = "Lv." + (Level + 1);
        switch(Data.Type)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                T_Desc.text = string.Format(Data.S_ItemDesc,Data.F_Dmgs[Level] * 10,Data.I_Counts[Level]);
                    break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
            case ItemData.ItemType.GunRateUp:
                T_Desc.text = string.Format(Data.S_ItemDesc, Data.F_Dmgs[Level] * 10);
                break;
            default:
                T_Desc.text = string.Format(Data.S_ItemDesc);
                break;
        }    
    }

    public void OnClickBtn()
    {
        switch(Data.Type)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(Level == 0)
                {
                    GameObject newweapon = new GameObject();
                    Weapon = newweapon.AddComponent<Weapon>();
                    Weapon.Init(Data);

                    GameManager.Instance.PlayerLogic.WeaponList.Add(Weapon);
                }
                else
                {
                    float nextdmg = Data.F_BaseDmg;
                    int nextcount = 0;

                    nextdmg += Data.F_BaseDmg + Data.F_Dmgs[Level];
                    nextcount += Data.I_Counts[Level];

                    Weapon.LevelUp(nextdmg, nextcount);
                }
                Level++;
                break;

            case ItemData.ItemType.Shoe:
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.GunRateUp:
                if(Level == 0)
                {
                    GameObject newgear = new GameObject();
                    Gear = newgear.AddComponent<Gear>();
                    Gear.Init(Data);
                }
                else
                {
                    float nextrate = Data.F_Dmgs[Level];
                    Gear.LevelUP(nextrate);
                }
                Level++;
                break;

            case ItemData.ItemType.Heal:
                GameManager.Instance.Health = GameManager.Instance.MaxHealth;
                break;
        }

        if(Level == Data.F_Dmgs.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
