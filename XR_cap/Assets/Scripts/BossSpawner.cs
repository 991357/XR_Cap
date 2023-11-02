using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawner : MonoBehaviour
{
    public Transform[] TM_SpawnPoint;
    public float F_Timer;
    public float SpawnTimer;
    public int I_Level;
    public float SpawnDelay;

    //bool Is

    public Text BossText;

    public GameObject BossEnter;
    public GameObject BossName;

    //public float F_LevelTime;

    private void Awake()
    {
        TM_SpawnPoint = GetComponentsInChildren<Transform>();
        //F_LevelTime = GameManager.Instance.F_MaxGameTime / C_SpawnData.Length;
    }
    void Update()
    {
        if (!GameManager.Instance.IsLive)
            return;

        if (GameManager.Instance.C_Manager.IsAction)
            return;
        else
        {
            F_Timer += Time.deltaTime;
            SpawnTimer += Time.deltaTime;
        }
        
        //I_Level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.F_GameTime / F_LevelTime), C_SpawnData.Length - 1);

        LevelCal();
        DelayCal();

        if (SpawnTimer > SpawnDelay)
        {
            SpawnTimer = 0;
            Spawn();
        }
    }
    void DelayCal()
    {
        if (Input.GetKey(KeyCode.X))
        {
            I_Level = 4;
            SpawnDelay = 0.01f;
        }
        else
        {
            if (I_Level == 1)
                SpawnDelay = 9f;
            else if (I_Level == 2)
                //SpawnDelay = 10.5f;
                SpawnDelay = 19f;
            else if (I_Level == 3)
                //SpawnDelay = 10.2f;
                SpawnDelay = 29f;
            else
                SpawnDelay = 25;
        }
    }


    void LevelCal()
    {
        if (F_Timer <= 15)
            I_Level = 1;
        else if (F_Timer <= 30)
            I_Level = 2;
        else if (F_Timer <= 60)
            I_Level = 3;
        else
            I_Level = 4;
    }


    void Spawn()
    {
        if (I_Level == 1)
        {
            //Enemy 1 Spawn
            GameObject Enemy = GameManager.Instance.P_Manager.Get(6);
            Enemy.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;
            BossEnter.SetActive(true);
            BossName.SetActive(true);
            StartCoroutine(TurnOffImage());
        }
        else if (I_Level == 2)
        {
            //Enemy 2 Spawn
            GameObject Enemy = GameManager.Instance.P_Manager.Get(7);
            Enemy.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;
            BossEnter.SetActive(true);
            BossName.SetActive(true);
            StartCoroutine(TurnOffImage());
        }
        else if( I_Level == 3)
        {
            //Enemy 3 Spawn
            GameObject Enemy = GameManager.Instance.P_Manager.Get(8);
            Enemy.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;
            BossEnter.SetActive(true);
            BossName.SetActive(true);
            StartCoroutine(TurnOffImage());
        }
        else if(I_Level == 4)
        {
            GameObject BossA = GameManager.Instance.P_Manager.Get(6);
            BossA.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;

            GameObject BossB = GameManager.Instance.P_Manager.Get(7);
            BossB.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;

            GameObject BossC = GameManager.Instance.P_Manager.Get(8);
            BossC.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;
        }
    }

    IEnumerator TurnOffImage()
    {
        yield return new WaitForSeconds(3);
        BossEnter.SetActive(false);
        BossName.SetActive(false);
    }
}