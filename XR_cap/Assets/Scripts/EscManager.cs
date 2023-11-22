using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscManager : MonoBehaviour
{
    //경과시간
    //처치한 적
    //처치한 보스
    //구출한 백혈구 (퀘스트 수)
    //레벨
    //hp
    //속도

    public Text Timer;
    public Text EnemyKill;
    public Text BossKill;
    public Text Quest;
    public Text Level;
    public Text HP;

    int Hp;

    public GameObject[] SpeedIcons;

    private void Update()
    {
        float remaintime = GameManager.Instance.MaxGameTime - GameManager.Instance.GameTime;
        int min = Mathf.FloorToInt(remaintime / 60);
        int sec = Mathf.FloorToInt(remaintime % 60);
        Timer.text = string.Format("{0:D2}:{1:D2}", min, sec);

        EnemyKill.text = GameManager.Instance.Kill.ToString();

        BossKill.text = GameManager.Instance.BossKillCount.ToString();

        Quest.text = GameManager.Instance.Q_Manager.QuestCount.ToString();

        Level.text = GameManager.Instance.Level.ToString();

        Hp = (int)GameManager.Instance.Health;
        HP.text = Hp.ToString();

        switch(GameManager.Instance.LevelUp.items[3].Level)
        {
            case 0:
                break;
            case 1:
                SpeedIcons[0].SetActive(true);
                break;
            case 2:
                SpeedIcons[1].SetActive(true);
                break;
            case 3:
                SpeedIcons[2].SetActive(true);
                break;
            case 4:
                SpeedIcons[3].SetActive(true);
                break;
            case 5:
                SpeedIcons[4].SetActive(true);
                break;
        }
    }
}
