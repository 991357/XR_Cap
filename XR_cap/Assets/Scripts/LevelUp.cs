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
        items = GetComponentsInChildren<Item>();
    }

    public void Show()
    {
        IsLevelUp = true;
        Next();
        IsEmpty();
        RT_rect.localScale = Vector3.one;
        GameManager.Instance.Stop();
        //GameManager.Instance.Obj_Health.SetActive(false);

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

    private void IsEmpty()
    {
        if (!items[0].gameObject.activeSelf && !items[1].gameObject.activeSelf && !items[2].gameObject.activeSelf && !items[3].gameObject.activeSelf && !items[4].gameObject.activeSelf && !items[5].gameObject.activeSelf && !items[6].gameObject.activeSelf && !items[7].gameObject.activeSelf && !items[9].gameObject.activeSelf && !items[9].gameObject.activeSelf)
        {
            Debug.Log("�ƹ��͵� �ȶ���");
            items[4].gameObject.SetActive(true);
        }
    }

    private void Next()
    {
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
            else if (GameManager.Instance.Player.WeaponList.Count == 4)
            {
                if(ranitem.Level == 0)
                {                
                    ranitem.gameObject.SetActive(false);
                }
                else if(ranitem.Level == 1)
                {
                    ranitem.gameObject.SetActive(true);
                }
                else
                {
                    if (ranitem.Level >= 4)
                        ranitem.gameObject.SetActive(false);
                    else
                        ranitem.gameObject.SetActive(true);
                }
            }
            else
                ranitem.gameObject.SetActive(true);
            //Debug.Log(GameManager.Instance.S_Player.transform.Find("Weapon0").gameObject.activeSelf);
        }
    }
}
