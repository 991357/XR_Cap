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

    public GameObject Language;
    public int LNum;

    public Dictionary<int, string[]> TalkData;

    private void Awake()
    {        
        TalkData = new Dictionary<int, string[]>();
    }

    private void Start()
    {
        Language = GameObject.Find("Language");
        //LNum = Language.GetComponent<LanguageNum>().Lnum;

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
        if (LNum == 0)
        {
            TalkData.Add(1000, new string[] { "안녕?", "여긴 처음이구나?", "우선 저것들좀 잡아줄래?", "퀘스트를 완료하면 다시 말을 걸어줘 !", "그럼 선물을 줄게" }); ;
            TalkData.Add(1001, new string[] { "퀘스트를 완료하면 다시 말을 걸어줘 !" });
            TalkData.Add(1002, new string[] { "다시 나를 찾아와줘!" });

            TalkData.Add(2000, new string[] { "잘 찾았네?! 주변에 있는것들좀 잡아줄래?" });
            TalkData.Add(2001, new string[] { "퀘스트를 완료하면 다시 말을 걸어줘 !" });
            TalkData.Add(2002, new string[] { "이걸 성공했다고..?" });
        }
        else if(LNum == 1)
        {
            TalkData.Add(1000, new string[] { "Hello?", "It's your first time here?", "Would you kill them first?", "Talk to me again when you complete the quest !", "Then I'll give you a present" }); ;
            TalkData.Add(1001, new string[] { "Talk to me again when you complete the quest !" });
            TalkData.Add(1002, new string[] { "Come back to me!" });
            
            TalkData.Add(2000, new string[] { "You found me well! Can you kill the ones around me?" });
            TalkData.Add(2001, new string[] { "Talk to me again when you complete the quest !" });
            TalkData.Add(2002, new string[] { "You succeeded in this...?" });
        }
        else if (LNum == 2)
        {
            TalkData.Add(1000, new string[] { "こんにちは?", "ここに来るのは初めてですか？", "あそこにいる敵を殺してください", "クエストを完了したら、もう一度私に話しかけてください !", "それではプレゼントを差し上げます." }); ;
            TalkData.Add(1001, new string[] { "クエストを完了したら、もう一度私に話しかけてください !" });
            TalkData.Add(1002, new string[] { "私をまた訪ねてきてください！" });

            TalkData.Add(2000, new string[] { "よく見つけましたね！ 私の周りにいるモンスターを殺してくれますか？" });
            TalkData.Add(2001, new string[] { "クエストを完了したら、もう一度私に話しかけてください !" });
            TalkData.Add(2002, new string[] { "これに成功したのでしょうか…？" });
        }
        else if (LNum == 3)
        {
            TalkData.Add(1000, new string[] { "你好?", "您第一次来这里吗?", "你能先杀了那些怪兽吗？", "完成任务后，再跟我说话！", "那我送你礼物吧" }); ;
            TalkData.Add(1001, new string[] { "完成任务后，再跟我说话！" });
            TalkData.Add(1002, new string[] { "再来找我吧!" });

            TalkData.Add(2000, new string[] { "你找对了！ 你能杀掉我周围的怪兽吗？" });
            TalkData.Add(2001, new string[] { "完成任务后，再跟我说话！" });
            TalkData.Add(2002, new string[] { "这个成功了吗...？" });
        }
        else if (LNum == 4)
        {
            TalkData.Add(1000, new string[] { "Hola!", "¿Es tu primera vez aquí?", "¿Los matarías primero?", "¡Háblame de nuevo cuando termine la búsqueda!", "Entonces te daré un regalo." }); ;
            TalkData.Add(1001, new string[] { "¡Háblame de nuevo cuando termine la búsqueda!" });
            TalkData.Add(1002, new string[] { "¡Regresa a mí!" });

            TalkData.Add(2000, new string[] { "¿Puedes matar al monstruo que me rodea? "});
            TalkData.Add(2001, new string[] { "¡Háblame de nuevo cuando termine la búsqueda!" });
            TalkData.Add(2002, new string[] { "¿Lo lograste?" });
        }
        else
        {
            TalkData.Add(1000, new string[] { "안녕?", "여긴 처음이구나?", "우선 저것들좀 잡아줄래?", "퀘스트를 완료하면 다시 말을 걸어줘 !", "그럼 선물을 줄게" }); ;
            TalkData.Add(1001, new string[] { "퀘스트를 완료하면 다시 말을 걸어줘 !" });
            TalkData.Add(1002, new string[] { "다시 나를 찾아와줘!" });

            TalkData.Add(2000, new string[] { "잘 찾았네?! 주변에 있는것들좀 잡아줄래?" });
            TalkData.Add(2001, new string[] { "퀘스트를 완료하면 다시 말을 걸어줘 !" });
            TalkData.Add(2002, new string[] { "이걸 성공했다고..?" });
        }
    }

    private void Update()
    {
       if(LNum == 0 )
        {
            TalkData[1000] = new string[] { "안녕?", "여긴 처음이구나?", "우선 저것들좀 잡아줄래?", "퀘스트를 완료하면 다시 말을 걸어줘 !", "그럼 선물을 줄게" };
            TalkData[1001] = new string[] { "퀘스트를 완료하면 다시 말을 걸어줘 !" };
            TalkData[1002] = new string[] { "다시 나를 찾아와줘!" };

            TalkData[2000] = new string[] { "잘 찾았네?! 주변에 있는것들좀 잡아줄래?" };
            TalkData[2001] = new string[] { "퀘스트를 완료하면 다시 말을 걸어줘 !" };
            TalkData[2002] = new string[] { "이걸 성공했다고..?" };
        }
       else if(LNum == 1)
        {
            TalkData[1000] = new string[] { "Hello?", "It's your first time here?", "Would you kill them first?", "Talk to me again when you complete the quest !", "Then I'll give you a present" };
            TalkData[1001] = new string[] { "Talk to me again when you complete the quest !" };
            TalkData[1002] = new string[] { "Come back to me!" };

            TalkData[2000] = new string[] { "You found me well! Can you kill the ones around me?" };
            TalkData[2001] = new string[] { "Talk to me again when you complete the quest !" };
            TalkData[2002] = new string[] { "You succeeded in this...?" };
        }
        else if (LNum == 2)
        {
            TalkData[1000] = new string[] { "こんにちは?", "ここに来るのは初めてですか？", "あそこにいる敵を殺してください", "クエストを完了したら、もう一度私に話しかけてください !", "それではプレゼントを差し上げます." };
            TalkData[1001] = new string[] { "クエストを完了したら、もう一度私に話しかけてください !" };
            TalkData[1002] = new string[] { "私をまた訪ねてきてください！" };

            TalkData[2000] = new string[] { "よく見つけましたね！ 私の周りにいるモンスターを殺してくれますか？" };
            TalkData[2001] = new string[] { "クエストを完了したら、もう一度私に話しかけてください !" };
            TalkData[2002] = new string[] { "これに成功したのでしょうか…？" };
        }
        else if (LNum == 3)
        {
            TalkData[1000] = new string[] { "你好?", "您第一次来这里吗?", "你能先杀了那些怪兽吗？", "完成任务后，再跟我说话！", "那我送你礼物吧" };
            TalkData[1001] = new string[] { "完成任务后，再跟我说话！" };
            TalkData[1002] = new string[] { "再来找我吧!" };

            TalkData[2000] = new string[] { "你找对了！ 你能杀掉我周围的怪兽吗？" };
            TalkData[2001] = new string[] { "完成任务后，再跟我说话！" };
            TalkData[2002] = new string[] { "这个成功了吗...？" };
        }
        else if (LNum == 4)
        {
            TalkData[1000] = new string[] { "Hola!", "¿Es tu primera vez aquí?", "¿Los matarías primero?", "¡Háblame de nuevo cuando termine la búsqueda!", "Entonces te daré un regalo." };
            TalkData[1001] = new string[] { "¡Háblame de nuevo cuando termine la búsqueda!" };
            TalkData[1002] = new string[] { "¡Regresa a mí!" };

            TalkData[2000] = new string[] { "¿Puedes matar al monstruo que me rodea? " };
            TalkData[2001] = new string[] { "¡Háblame de nuevo cuando termine la búsqueda!" };
            TalkData[2002] = new string[] { "¿Lo lograste?" };
        }
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
