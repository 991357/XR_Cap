using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# GameObject")]
    public Player Player;
    public PoolManager P_Manager;
    public LevelUp LevelUp;
    public GameObject Obj_EnemyCleaner;
    public ChatManager C_Manager;
    public QuestManager Q_Manager;
    public GameObject EscPanel;
    public GameObject ResultPanel;
    public GameObject[] UltimateImg;
    public GameObject[] ResultUltimate;

    [Header("# GameControl")]
    public float GameTime;
    public float MaxGameTime = 2 * 10f;
    public bool IsLive;
    bool IsEsc;

    [Header("# PlayerInfo")]
    public float Health;
    public float MaxHealth = 100;
    public int PlayerId;
    public int Level;
    public int Kill;
    public int Exp;
    public int BossKillCount;
    public int[] I_NextExp = { 10,30,60,100,150,210,280,360,450,600};

    [Header("# Data")]
    public Image Blood_Img;


    private void Awake()
    {
        Instance = this;
    }

    public void GameStart(int id)
    {
        PlayerId = id;

        Health = MaxHealth;

        Player.gameObject.SetActive(true);
        LevelUp.Select(PlayerId);

        switch (PlayerId)
        {
            case 0:
                UltimateImg[0].SetActive(true);
                UltimateImg[2].SetActive(true);
                ResultUltimate[0].SetActive(true);
                break;
            case 1:
                UltimateImg[0].SetActive(true);
                UltimateImg[2].SetActive(true);
                ResultUltimate[0].SetActive(true);
                break;
            case 2:
                UltimateImg[1].SetActive(true);
                UltimateImg[3].SetActive(true);
                ResultUltimate[1].SetActive(true);
                break;
            default:
                break;
        }

        Resume();

        AudioManager.Instance.PlayBgm(true);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
        //IsLive = false;
    }

    IEnumerator GameOverRoutine()
    {
        IsLive = false;
    
        yield return new WaitForSeconds(1.2f);
        //yield return null;
    
        //Obj_UiResult.gameObject.SetActive(true);
        //Obj_UiResult.Lose();
    
        ResultPanel.SetActive(true);
    
        Stop();
    
        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameOverEnd()
    {
        ResultPanel.SetActive(true);
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        IsLive = false;

        yield return new WaitForSeconds(2f);

        //Obj_UiResult.gameObject.SetActive(true);
        //Obj_UiResult.Win();
        ResultPanel.SetActive(true);

        Stop();

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Win);
    }

    private void Update()
    {
        if (!IsLive)
            return;

        if (C_Manager.IsAction)
            return;

        GameTime += Time.deltaTime;

        if(GameTime > MaxGameTime)
        {
            Obj_EnemyCleaner.SetActive(true);
            GameTime = MaxGameTime;
            GameVictory();
        }
        Dummy_Dash();
        EscMenu();
        Dummy_BloodImg();
    }

    public void Dummy_Dash()
    {
        //if(Player.DashTimer < Player.DashCoolTime)
        //{
        //    Dummy_T_DashCoolTime.text = "Dash : false";
        //}
        //else
        //{
        //    Dummy_T_DashCoolTime.text = "Dash : true";
        //}
        //
        //if(Player.QTimer < Player.QCoolTime)
        //{
        //    Dummy_T_QCoolTier.text = "Q : False";
        //}
        //else
        //{
        //    Dummy_T_QCoolTier.text = "Q : true";
        //}
        //
        //Dummy_T_Power.text = "Power : " + Player.Power;
    }

    public void Dummy_BloodImg()
    {
        if(Health <= 40)
        {
            StartCoroutine(Blooding());
        }
    }
    IEnumerator Blooding()
    {
        while (Health > 50)
        {
            for (byte i = 0; i < 255; i++)
            {
                Blood_Img.color = new Color32(255, 255, 255, i);
            }
            yield return new WaitForSeconds(1.5f);
            for (byte i = 255; i >0; i--)
            {
                Blood_Img.color = new Color32(255, 255, 255, i);
                break;
            }
        }
    }

    public void GetExp(int exp)
    {
        if (!IsLive)
            return;

        Exp += exp;
        if(Exp == I_NextExp[Mathf.Min(Level,I_NextExp.Length-1)])
        {
            Level++;
            Exp = 0;
            //Weapon.LevelUp(5, 1);
            LevelUp.Show();
        }
    }
    
    public void Stop()
    {
        IsLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        IsLive = true;
        Time.timeScale = 1;
    }
    
    void EscMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EscPanel.SetActive(true);
            Stop();
        }
    }
    
    public void OnClickEscExitBtn()
    {      
        EscPanel.SetActive(false);
        Resume();
    }
}
