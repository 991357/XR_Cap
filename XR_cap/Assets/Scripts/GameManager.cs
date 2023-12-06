using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

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
    public GameObject CharPanel;
    public GameObject SucceseeImage;
    public GameObject FadeIn;
    public AudioManager A_Manager;
    public SfxManager SfxManager;
    public AchiveManager AchiveManagerRef;
    public RectTransform EscGroupRt;

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
    public int UltimateKillCount;
    public int Exp;
    public int BossKillCount;
    public int[] I_NextExp = { 10,30,60,100,150,210,280,360,450,600};

    [Header("# Data")]
    public Image Blood_Img;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1;
        AudioManager.Instance.PlayBgm(true);
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
    }

    public void GameStart(int id)
    {
        PlayerId = id;
        //Invoke("StartGame", 1.8f);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.8f);
        Debug.Log("눌림");
        FadeIn.GetComponent<Animator>().SetTrigger("In");
        CharPanel.SetActive(false);
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
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Lose);
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
        SucceseeImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        SucceseeImage.SetActive(false);

        ResultPanel.SetActive(true);

        Stop();

        AudioManager.Instance.PlayBgm(false);
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Win);
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
        EscMenu();
        //Dummy_BloodImg();

        UltimateCoinCheck();
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
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Resume();
    }

    public void UltimateCoinCheck()
    {
        if((UltimateKillCount / 2) >= 50)
        {
            if (Player.UltimateCoin == 1)
                return;

            Player.UltimateCoin++;

            if(PlayerId == 0 || PlayerId == 1)
            {
                //파랑 파티클
                GameObject bluePar = P_Manager.Get(38);
                bluePar.transform.position = Player.transform.position;

                SfxManager.Instance.PlaySfx(SfxManager.Sfx.UltimateFilled);

                StartCoroutine(OffPar(bluePar));
            }
            else
            {
                //빨강 파티클
                GameObject redPar = P_Manager.Get(39);
                redPar.transform.position = Player.transform.position;

                SfxManager.Instance.PlaySfx(SfxManager.Sfx.UltimateFilled);

                StartCoroutine(OffPar(redPar));
            }
            UltimateKillCount = 0;
        }
    }

    IEnumerator OffPar(GameObject Par)
    {
        yield return new WaitForSeconds(0.5f);
        Par.SetActive(false);
    }
}
