using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public int TalkIndex;

    public QuestManager Q_Manager;

    public GameObject TalkPanel;
    public Text TalkText;
    public GameObject ScanObject;
    public bool IsAction;
    public GameObject Icon;

    Dictionary<int, string[]> TalkData;

    private void Awake()
    {
        TalkData = new Dictionary<int, string[]>();
        InitData();
    }

    public void Action(GameObject obj)
    {
        ScanObject = obj;
        NPC npc = ScanObject.GetComponent<NPC>();
        Talk(npc.Id);

        TalkPanel.SetActive(IsAction);
    }

    void InitData()
    {
        TalkData.Add(1000, new string[] {"�ȳ�?","���� ó���̱���?","�켱 ���͵��� ����ٷ�?","����Ʈ�� �Ϸ��ϸ� �ٽ� ���� �ɾ��� !" , "�׷� ������ �ٰ�"});;
        TalkData.Add(1001, new string[] { "����Ʈ�� �Ϸ��ϸ� �ٽ� ���� �ɾ��� !" });
        TalkData.Add(1002, new string[] {"�ٽ� ���� ã�ƿ���!"});

        TalkData.Add(2000, new string[] { "�� ã�ҳ�?! �ֺ��� �ִ°͵��� ����ٷ�?" });
        TalkData.Add(2001, new string[] { "����Ʈ�� �Ϸ��ϸ� �ٽ� ���� �ɾ��� !" });
        TalkData.Add(2002, new string[] { "�̰� �����ߴٰ�..?" });
    }

    public string GetTalk(int id, int index)
    {
        if (TalkIndex == TalkData[id].Length)
            return null;
        else
            return TalkData[id][index];
    }

    void Talk(int id)
    {
        if (!TalkData.ContainsKey(id))
            return;

        string data = GetTalk(id, TalkIndex);

        if (data == null)
        {
            IsAction = false;
            Icon.SetActive(false);
            TalkIndex = 0;

            if (id == 1000 || id == 1002 || id == 2000 || id == 2002)
            {
                NPC npc = ScanObject.GetComponent<NPC>();
                npc.Id += 1;
            }

            //NPC npc = ScanObject.GetComponent<NPC>();
            //npc.Id += 1;
            //if (npc.Id == 1001)
            //{
            //    GameManager.Instance.Q_Manager.IsQuest = true;
            //    GameManager.Instance.Q_Manager.Id = 1;
            //    Debug.Log("����Ʈ ����!");
            //}
            //else if(npc.Id == 2001)
            //{
            //    Debug.Log("�� ���� ����Ʈ�� �����");
            //}

            return;
        }
        else
        {
            TalkText.text = data;
            Icon.SetActive(true);
            IsAction = true;
            TalkIndex++;
        }
    }

    void TalkEnd(int id)
    {
        //id�� ���� ����� talk�� ��������
        //Quest �ޱ�
        //�ٵ� �̹� Quest ��Ȳ�̶��?return
    }
}
