using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string Name;
    public int Id;
    public GameObject QuestNotice;
    public GameObject NPC2;

    public bool IsEnd;

    private void Update()
    {
        QuestStart();
        //HoldId();
    }

    void QuestStart()
    {
        if(Id == 1001)
        {
            GameManager.Instance.Q_Manager.IsQuest = true;

            //�� 5���� ���
            GameManager.Instance.Q_Manager.Id = 1;

            QuestNotice.SetActive(true);
            QuestNotice.transform.GetChild(0).GetComponent<Text>().text = "Kill Monster\n" + GameManager.Instance.Q_Manager.Count + "/5";

            //ó��
            if(GameManager.Instance.Q_Manager.Count == 5)
                QuestNotice.SetActive(false);
        }
        ActiveFalse();
        NextNPC();

        if (Id == 2001)
        {
            GameManager.Instance.Q_Manager.IsHint = false;

            GameManager.Instance.Q_Manager.IsQuest = true;

            GameManager.Instance.Q_Manager.Id = 2;

            QuestNotice.SetActive(true);
            QuestNotice.transform.GetChild(0).GetComponent<Text>().text = "Kill Monster\n" + GameManager.Instance.Q_Manager.Count + "/5000";

            //ó��
            if (GameManager.Instance.Q_Manager.Count == 5000)
                QuestNotice.SetActive(false);
        }
    }

    void ActiveFalse()
    {
        if (Id == 1003)
        {
            GameManager.Instance.Q_Manager.IsHint = true;
            NPC2.gameObject.SetActive(true);
            NPC2.transform.position = new Vector3(Random.Range(-40, 40), Random.Range(-40, 40), 0);
            gameObject.SetActive(false);
            IsEnd = true;
        }

        if (Id == 2003)
        {
            gameObject.SetActive(false);
        }
    }

    void NextNPC()
    {
        if(IsEnd)
        {
            //���ο� NPC�� ���� ��Ŵ
            if (Id == 2000)
            {
                IsEnd = false;
            }
            //��ġ �ʱ�ȭ
            
        }
    }
}
