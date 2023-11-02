using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] TM_SpawnPoint;
    public float F_Timer;
    public float SpawnTimer;
    public int I_Level;
    public float SpawnDelay;

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

    
    void LevelCal()
    {
        if (F_Timer <= 15)
        {
            I_Level = 1;
            SpawnDelay = Random.Range(1,1.5f);
        }
        else if (F_Timer <= 30)
        {
            I_Level = 2;
            StartCoroutine(LevelChange());
        }
        else if (F_Timer <= 60)
        {
            I_Level = 3;
            StartCoroutine(LevelChange());
        }
        else if(F_Timer <= 100)
        {
            I_Level = 4;
            SpawnDelay = 0.45f;
        }
        else
        {
            SpawnDelay = 0.3f;
        }
    }

    void DelayCal()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            I_Level = 4;
            SpawnDelay = 0.01f;
        }
    }

    IEnumerator LevelChange()
    {
        SpawnDelay = 0.5f;

        yield return new WaitForSeconds(0.01f);
        if(I_Level == 2)
            SpawnDelay = Random.Range(0.8f, 1f);
        else if(I_Level == 3)
            SpawnDelay = Random.Range(0.6f, 0.8f);
    }
    void Spawn()
    {
        if(I_Level == 1)
        {
            //Enemy 1 Spawn
            GameObject Enemy = GameManager.Instance.P_Manager.Get(0);
            Enemy.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;
        }
        else if(I_Level == 2)
        {
            //Enemy 2 Spawn
            GameObject Enemy = GameManager.Instance.P_Manager.Get(4);
            Enemy.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;

            GameObject Enemya = GameManager.Instance.P_Manager.Get(0);
            Enemya.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;

        }
        else
        {
            GameObject EnemyA = GameManager.Instance.P_Manager.Get(0);
            EnemyA.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;

            GameObject EnemyB = GameManager.Instance.P_Manager.Get(4);
            EnemyB.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;

            GameObject EnemyC = GameManager.Instance.P_Manager.Get(5);
            EnemyC.transform.position = TM_SpawnPoint[Random.Range(1, TM_SpawnPoint.Length)].position;
        }
    }
}