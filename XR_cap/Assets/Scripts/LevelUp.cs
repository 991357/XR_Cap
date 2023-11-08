using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform RT_rect;
    public Item[] items;
    public GameObject Player;
    public GameObject TrashCard;

    public bool IsLevelUp;
    private void Awake()
    {
        RT_rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        IsLevelUp = true;
        Next();
        RT_rect.localScale = Vector3.one;
        GameManager.Instance.Stop();
        GameManager.Instance.Obj_Health.SetActive(false);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.Instance.EffectBgm(true);
    }

    public void Hide()
    {
        IsLevelUp = false;
        RT_rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();
        GameManager.Instance.Obj_Health.SetActive(true);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.Instance.EffectBgm(false);
    }

    public void Select(int i)
    {
        items[i].OnClickBtn();
    }


    private void Next()
    {
        Weapon test = null;

        //��� ������ ��Ȱ��ȭ
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        //�������� 3���� Ȱ��ȭ
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int i = 0; i < ran.Length; i++)
        {
            Item ranitem = items[ran[i]];

            //������ ��� �Һ���������� ��ü
            if (ranitem.Level == ranitem.Data.F_Dmgs.Length)
            {
                items[4].gameObject.SetActive(true);
                //�Һ�������� �������ϰ��
                //items[Random.Range(4,7)].gameObject.SetActive(true);���� �ڵ带 ���� ��
            }
            else if (ranitem == items[2])
            {
                // Test
                for (int j = 0; j < GameManager.Instance.PlayerLogic.WeaponList.Count; j++)
                {
                    if (GameManager.Instance.PlayerLogic.WeaponList[j].name == "Weapon0")
                    {
                        items[2].gameObject.SetActive(true);
                        break;
                    }
                    else
                    {
                        items[2].gameObject.SetActive(false);
                        //TrashCard.gameObject.SetActive(true);
                    }
                }
            }
            else if(ranitem == items[5])
            {
                for (int j = 0; j < GameManager.Instance.PlayerLogic.WeaponList.Count; j++)
                {
                    if (GameManager.Instance.PlayerLogic.WeaponList[j].name == "Weapon1")
                    {
                        items[5].gameObject.SetActive(true);
                        break;
                    }
                    else
                    {
                        items[5].gameObject.SetActive(false);
                        //TrashCard.gameObject.SetActive(true);
                    }
                }

            }
            else
                ranitem.gameObject.SetActive(true);
            //Debug.Log(GameManager.Instance.S_Player.transform.Find("Weapon0").gameObject.activeSelf);
        }
    }
}
