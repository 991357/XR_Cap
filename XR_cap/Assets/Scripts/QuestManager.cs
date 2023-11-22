using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int Id;
    public int Count;

    public int QuestCount;

    public bool IsQuest;
    public bool IsHint = false;

    public GameObject NPC2;

    public Text X;
    public Text Y;

    private void Update()
    {
        if (Id == 1)
        {
            if (GameManager.Instance.C_Manager.ScanObject.GetComponent<NPC>().Id == 1001)
            {
                if (Count >= 5)
                {
                    Count = 0;
                    Debug.Log("Äù½ºÆ® ¼º°ø!");
                    QuestCount++;
                    GameManager.Instance.C_Manager.ScanObject.GetComponent<NPC>().Id += 1;
                    GameManager.Instance.Player.Power++;
                    IsQuest = false;
                }
            }
        }

        FindNPC();

        if (Id == 2)
        {
            if (GameManager.Instance.C_Manager.ScanObject.GetComponent<NPC>().Id == 2001)
            {
                if (Count >= 5000)
                {
                    Count = 0;
                    Debug.Log("Äù½ºÆ® ¼º°ø!");
                    QuestCount++;
                    GameManager.Instance.C_Manager.ScanObject.GetComponent<NPC>().Id += 1;
                    GameManager.Instance.Player.Power++;
                    IsQuest = false;
                }
            }
        }

    }

    void FindNPC()
    {
        if (IsHint)
        {
            X.gameObject.SetActive(true);
            Y.gameObject.SetActive(true);
            float x = NPC2.transform.position.x - GameManager.Instance.Player.transform.position.x;
            float y = NPC2.transform.position.y - GameManager.Instance.Player.transform.position.y;
            x = (int)x;
            y = (int)y;

            X.text = "X : " + x.ToString();
            Y.text = "Y : " + y.ToString();
        }
    }
}