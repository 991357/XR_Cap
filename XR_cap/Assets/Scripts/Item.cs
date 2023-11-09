
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
            case ItemData.ItemType.Ice_1:
                switch (Level)
                {
                    case 0:
                        T_Desc.text = "플레이어 몸을 휘감는 칼을\n 획득합니다.";
                        break;
                    case 1:
                        T_Desc.text = "갯수가 증가합니다.\n크기가 증가합니다.\n데미지가" + Data.F_Dmgs[Level] * 10 +"% 증가합니다.";
                        break;
                    case 2:
                        T_Desc.text = "칼에서 검기를 발사합니다.\n데미지가 " + (Data.F_Dmgs[Level] * 10) + "% 증가합니다.";
                        break;
                    case 3:
                        T_Desc.text = "검기에 맞은 적 위치에\n 빙산을 생성합니다.\n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가합니다.";
                        break;
                    default:
                        T_Desc.text = "&*&@$#%^$&*#@!@#*&^%!@@##^&%$@^#@$!";
                        break;
                }
                break;


            case ItemData.ItemType.Ice_2:
                //T_Desc.text = string.Format(Data.S_ItemDesc,Data.F_Dmgs[Level] * 10,Data.I_Counts[Level]);
                switch (Level)
                {
                    case 0:
                        T_Desc.text = "플레이어를 중심으로 \n회전하는 고드름을 생성합니다.";
                        break;
                    case 1:
                        T_Desc.text = "갯수가 증가합니다.\n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가합니다.";
                        break;
                    case 2:
                        T_Desc.text = "갯수가 증가합니다.\n속도가 증가합니다.\n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가합니다.";
                        break;
                    case 3:
                        T_Desc.text = "플레이어 주변에\n냉기지대가 형성됩니다. \n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가합니다.";
                        break;
                    default:
                        T_Desc.text = "*&#^%T#%*!@%^%#@^*@$#^%*";
                
                        break;
                }
                break;


            case ItemData.ItemType.Shoe:
            //case ItemData.ItemType.GunRateUp:
                T_Desc.text = string.Format(Data.S_ItemDesc, Data.F_Dmgs[Level] * 10);
                break;


            case ItemData.ItemType.Fire_1:
                switch (Level)
                {
                    case 0:
                        T_Desc.text = "파이어볼을 발사합니다";
                        break;
                    case 1:
                        T_Desc.text = "발사체 갯수가\n2개로 증가합니다.\n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가합니다.";
                        break;
                    case 2:
                        T_Desc.text = "발사체 크기가 증가합니다\n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가합니다.";
                        break;
                    case 3:
                        T_Desc.text = "발사체에 맞은 적 주변으로\n파편이 발생합니다\n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가합니다.";
                        break;
                
                    default:
                        T_Desc.text = "*&#^%T#%*!@%^%#@^*@$#^%*";
                        break;
                }
                break;

            case ItemData.ItemType.Fire_2:
                switch (Level)
                {
                    case 0:
                        T_Desc.text = "화염지대를 생성합니다";
                        break;
                    case 1:
                        T_Desc.text = "갯수가 증가합니다\n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가합니다.";
                        break;
                    case 2:
                        T_Desc.text = "화염지대에서 \n 폭발을 일으킵니다.\n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가합니다.";
                        break;
                    case 3:
                        T_Desc.text = "화염지대 생성시\n원형파동이 생성됩니다.\n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가합니다.";
                        break;
                    default:
                        break;
                }
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
            case ItemData.ItemType.Ice_1:
            case ItemData.ItemType.Ice_2:
            case ItemData.ItemType.Fire_1:
            case ItemData.ItemType.Fire_2:
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
