
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
                        T_Desc.text = "�÷��̾� ���� �ְ��� Į��\n ȹ���մϴ�.";
                        break;
                    case 1:
                        T_Desc.text = "������ �����մϴ�.\nũ�Ⱑ �����մϴ�.\n��������" + Data.F_Dmgs[Level] * 10 +"% �����մϴ�.";
                        break;
                    case 2:
                        T_Desc.text = "Į���� �˱⸦ �߻��մϴ�.\n�������� " + (Data.F_Dmgs[Level] * 10) + "% �����մϴ�.";
                        break;
                    case 3:
                        T_Desc.text = "�˱⿡ ���� �� ��ġ��\n ������ �����մϴ�.\n�������� " + Data.F_Dmgs[Level] * 10 + "% �����մϴ�.";
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
                        T_Desc.text = "�÷��̾ �߽����� \nȸ���ϴ� ��帧�� �����մϴ�.";
                        break;
                    case 1:
                        T_Desc.text = "������ �����մϴ�.\n�������� " + Data.F_Dmgs[Level] * 10 + "% �����մϴ�.";
                        break;
                    case 2:
                        T_Desc.text = "������ �����մϴ�.\n�ӵ��� �����մϴ�.\n�������� " + Data.F_Dmgs[Level] * 10 + "% �����մϴ�.";
                        break;
                    case 3:
                        T_Desc.text = "�÷��̾� �ֺ���\n�ñ����밡 �����˴ϴ�. \n�������� " + Data.F_Dmgs[Level] * 10 + "% �����մϴ�.";
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
                        T_Desc.text = "���̾�� �߻��մϴ�";
                        break;
                    case 1:
                        T_Desc.text = "�߻�ü ������\n2���� �����մϴ�.\n�������� " + Data.F_Dmgs[Level] * 10 + "% �����մϴ�.";
                        break;
                    case 2:
                        T_Desc.text = "�߻�ü ũ�Ⱑ �����մϴ�\n�������� " + Data.F_Dmgs[Level] * 10 + "% �����մϴ�.";
                        break;
                    case 3:
                        T_Desc.text = "�߻�ü�� ���� �� �ֺ�����\n������ �߻��մϴ�\n�������� " + Data.F_Dmgs[Level] * 10 + "% �����մϴ�.";
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
                        T_Desc.text = "ȭ�����븦 �����մϴ�";
                        break;
                    case 1:
                        T_Desc.text = "������ �����մϴ�\n�������� " + Data.F_Dmgs[Level] * 10 + "% �����մϴ�.";
                        break;
                    case 2:
                        T_Desc.text = "ȭ�����뿡�� \n ������ ����ŵ�ϴ�.\n�������� " + Data.F_Dmgs[Level] * 10 + "% �����մϴ�.";
                        break;
                    case 3:
                        T_Desc.text = "ȭ������ ������\n�����ĵ��� �����˴ϴ�.\n�������� " + Data.F_Dmgs[Level] * 10 + "% �����մϴ�.";
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
