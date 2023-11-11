
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
                        T_Desc.text = "�÷��̾� ���� �ְ��� Į\n ȹ��";
                        break;
                    case 1:
                        T_Desc.text = "���� ����\nũ�� 30% ����\n������ " + Data.F_Dmgs[Level] * 10 +"% ����";
                        break;
                    case 2:
                        T_Desc.text = "�˱� ����\n������ " + (Data.F_Dmgs[Level] * 10) + "% ����";
                        break;
                    case 3:
                        T_Desc.text = "�ǰ���ġ�� ���� ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
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
                        T_Desc.text = "�÷��̾� �߽����� \nȸ���ϴ� ��帧 ����";
                        break;
                    case 1:
                        T_Desc.text = "���� ����\n�������� " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 2:
                        T_Desc.text = "���� ����\nȸ�� �ӵ� 2�� ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 3:
                        T_Desc.text = "�÷��̾� �ֺ�\n�ñ����� ���� \n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
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
                        T_Desc.text = "���̾ �߻�";
                        break;
                    case 1:
                        T_Desc.text = "�߻�ü 2�� ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 2:
                        T_Desc.text = "�߻�ü ũ�� ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 3:
                        T_Desc.text = "�ǰ� �� ���� ������\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
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
                        T_Desc.text = "ȭ������ ����";
                        break;
                    case 1:
                        T_Desc.text = "������ ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 2:
                        T_Desc.text = "ȭ�����뿡�� ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 3:
                        T_Desc.text = "ȭ������ ������\n�����ĵ� ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
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
                        T_Desc.text = "�����ϴ� ���� �߻�";
                        break;
                    case 1:
                        T_Desc.text = "���� Ƚ�� ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 2:
                        T_Desc.text = "���� �� ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 3:
                        T_Desc.text = "���� �߰�\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
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
                        T_Desc.text = "���ϰ� �߻�";
                        break;
                    case 1:
                        T_Desc.text = "ũ�� 30% ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 2:
                        T_Desc.text = "3���� �߻�\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 3:
                        T_Desc.text = "�ǰ� �� ��������\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
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
                        T_Desc.text = "�÷��̾ ��Ű��\nĮ ����";
                        break;
                    case 1:
                        T_Desc.text = "���� 2�� ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 2:
                        T_Desc.text = "���� 2�� ����\n�� ����\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
                        break;
                    case 3:
                        T_Desc.text = "���� 2�� ����\n���� �� ġ��Ÿ 30%\n������ " + Data.F_Dmgs[Level] * 10 + "% ����";
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
                        T_Desc.text = "���ο� �׹� �߻�";
                        break;
                    case 1:
                        T_Desc.text = "���� ����";
                        break;
                    case 2:
                        T_Desc.text = "�� ���� �����̻�";
                        break;
                    case 3:
                        T_Desc.text = "���� ��ź ����";
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
