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
        TalkData.Add(1000, new string[] {"안녕?","여긴 처음이구나?","우선 저것들좀 잡아줄래?","퀘스트를 완료하면 다시 말을 걸어줘 !" , "그럼 선물을 줄게"});;
        TalkData.Add(1001, new string[] { "퀘스트를 완료하면 다시 말을 걸어줘 !" });
        TalkData.Add(1002, new string[] {"다시 나를 찾아와줘!"});

        TalkData.Add(2000, new string[] { "잘 찾았네?! 주변에 있는것들좀 잡아줄래?" });
        TalkData.Add(2001, new string[] { "퀘스트를 완료하면 다시 말을 걸어줘 !" });
        TalkData.Add(2002, new string[] { "이걸 성공했다고..?" });
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
            //    Debug.Log("퀘스트 시작!");
            //}
            //else if(npc.Id == 2001)
            //{
            //    Debug.Log("앤 아직 퀘스트가 없어용");
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
        //id를 가진 사람과 talk가 끝났는지
        //Quest 받기
        //근데 이미 Quest 상황이라면?return
    }
}
