using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text Timer;
    public Text EnemyKill;
    public Text BossKill;
    public Text Save;
    public Text Level;
    public Text HP;
    int Hp;

    public GameObject[] SpeedIcons;


    private void OnEnable()
    {
        float remaintime = GameManager.Instance.MaxGameTime - GameManager.Instance.GameTime;
        int min = Mathf.FloorToInt(remaintime / 60);
        int sec = Mathf.FloorToInt(remaintime % 60);
        Timer.text = string.Format("{0:D2}:{1:D2}", min, sec);

        EnemyKill.text = GameManager.Instance.Kill.ToString();

        BossKill.text = GameManager.Instance.BossKillCount.ToString();

        Save.text = GameManager.Instance.Q_Manager.QuestCount.ToString();

        Level.text = GameManager.Instance.Level.ToString();

        Hp = (int)GameManager.Instance.Health;
        HP.text = Hp.ToString();

        switch (GameManager.Instance.LevelUp.items[3].Level)
        {
            case 0:
                break;
            case 1:
                SpeedIcons[0].SetActive(true);
                break;
            case 2:
                for (int i = 0; i < GameManager.Instance.LevelUp.items[3].Level; i++)
                    SpeedIcons[i].SetActive(true);
                break;
            case 3:
                for (int i = 0; i < GameManager.Instance.LevelUp.items[3].Level; i++)
                    SpeedIcons[i].SetActive(true);
                break;
            case 4:
                for (int i = 0; i < GameManager.Instance.LevelUp.items[3].Level; i++)
                    SpeedIcons[i].SetActive(true);
                break;
            case 5:
                for (int i = 0; i < GameManager.Instance.LevelUp.items[3].Level; i++)
                    SpeedIcons[i].SetActive(true);
                break;
        }
    }
}
