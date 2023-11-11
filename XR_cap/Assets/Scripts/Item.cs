
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
                        T_Desc.text = "플레이어 몸을 휘감는 칼\n 획득";
                        break;
                    case 1:
                        T_Desc.text = "갯수 증가\n크기 30% 증가\n데미지 " + Data.F_Dmgs[Level] * 10 +"% 증가";
                        break;
                    case 2:
                        T_Desc.text = "검기 생성\n데미지 " + (Data.F_Dmgs[Level] * 10) + "% 증가";
                        break;
                    case 3:
                        T_Desc.text = "피격위치에 빙산 생성\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
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
                        T_Desc.text = "플레이어 중심으로 \n회전하는 고드름 생성";
                        break;
                    case 1:
                        T_Desc.text = "갯수 증가\n데미지가 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 2:
                        T_Desc.text = "갯수 증가\n회전 속도 2배 증가\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 3:
                        T_Desc.text = "플레이어 주변\n냉기지대 형성 \n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
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
                        T_Desc.text = "파이어볼 발사";
                        break;
                    case 1:
                        T_Desc.text = "발사체 2개 증가\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 2:
                        T_Desc.text = "발사체 크기 증가\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 3:
                        T_Desc.text = "피격 시 파편 데미지\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
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
                        T_Desc.text = "화염지대 생성";
                        break;
                    case 1:
                        T_Desc.text = "갯수가 증가\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 2:
                        T_Desc.text = "화염지대에서 폭발\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 3:
                        T_Desc.text = "화염지대 생성시\n원형파동 생성\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    default:
                        T_Desc.text = "*&#^%T#%*!@%^%#@^*@$#^%*";
                        break;
                }
                break;

            case ItemData.ItemType.Thunder_1:
                switch (Level)
                {
                    case 0:
                        T_Desc.text = "폭발하는 전기 발사";
                        break;
                    case 1:
                        T_Desc.text = "폭발 횟수 증가\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 2:
                        T_Desc.text = "폭발 후 전이\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 3:
                        T_Desc.text = "낙뢰 추가\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    default:
                        T_Desc.text = "*&#^%T#%*!@%^%#@^*@$#^%*";
                        break;
                }
                break;

            case ItemData.ItemType.Thunder_2:
                switch (Level)
                {
                    case 0:
                        T_Desc.text = "레일건 발사";
                        break;
                    case 1:
                        T_Desc.text = "크기 30% 증가\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 2:
                        T_Desc.text = "3갈래 발사\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 3:
                        T_Desc.text = "피격 후 전기장판\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    default:
                        T_Desc.text = "*&#^%T#%*!@%^%#@^*@$#^%*";
                        break;
                }
                break;

            case ItemData.ItemType.Mes:
                switch (Level)
                {
                    case 0:
                        T_Desc.text = "플레이어를 지키는\n칼 생성";
                        break;
                    case 1:
                        T_Desc.text = "갯수 2개 증가\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 2:
                        T_Desc.text = "갯수 2개 증가\n적 출혈\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    case 3:
                        T_Desc.text = "갯수 2개 증가\n출혈 및 치명타 30%\n데미지 " + Data.F_Dmgs[Level] * 10 + "% 증가";
                        break;
                    default:
                        T_Desc.text = "*&#^%T#%*!@%^%#@^*@$#^%*";
                        break;
                }
                break;

            case ItemData.ItemType.SlowNet:
                switch (Level)
                {
                    case 0:
                        T_Desc.text = "슬로우 그물 발사";
                        break;
                    case 1:
                        T_Desc.text = "갯수 증가";
                        break;
                    case 2:
                        T_Desc.text = "적 랜덤 상태이상";
                        break;
                    case 3:
                        T_Desc.text = "장판 폭탄 생성";
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
            case ItemData.ItemType.Mes:
            case ItemData.ItemType.SlowNet:
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
