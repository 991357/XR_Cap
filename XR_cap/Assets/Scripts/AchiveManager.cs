using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] Obj_LockCharacter;
    public GameObject[] Obj_UnLockCharacter;

    public GameObject Obj_Notice;

    WaitForSecondsRealtime Wait;

    enum Achive { Unlock2, Unlock3}

    Achive[] Achives;

    private void Awake()
    {
        Achives = (Achive[])Enum.GetValues(typeof(Achive));
        Wait = new WaitForSecondsRealtime(5f);
        if (!PlayerPrefs.HasKey("MyData"))
            Init();
    }

    public void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in Achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    private void Start()
    {
        UnlockCharacter();
    }
    void UnlockCharacter()
    {
        for(int i = 0; i < Obj_LockCharacter.Length; i++)
        {
            string achivename = Achives[i].ToString();
            bool isunlock = PlayerPrefs.GetInt(achivename) == 1;
            Obj_LockCharacter[i].SetActive(!isunlock);
            Obj_UnLockCharacter[i].SetActive(isunlock);
        }
    }

    private void LateUpdate()
    {
        foreach(Achive achive in Achives)
        {
            CheckAchive(achive);
        }
    }
    
    void CheckAchive(Achive achive)
    {
        bool isachive = false;

        switch(achive)
        {
            case Achive.Unlock2:            //어떤 몹이든 10마리 처치시 해금 
                if (GameManager.Instance.IsLive)
                {
                    isachive = GameManager.Instance.Kill >= 15000;
                }
                break;
            //case Achive.Unlock3:            //어떤 캐릭터든 버티면 해금
            //    isachive = GameManager.Instance.F_GameTime == GameManager.Instance.F_MaxGameTime;
            //    break;
        }
        
        if(isachive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for(int i=0; i < Obj_Notice.transform.childCount; i++)
            {
                bool isactive = i == (int)achive;
                Obj_Notice.transform.GetChild(i).gameObject.SetActive(isactive);
            }

            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        Obj_Notice.SetActive(true);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);

        yield return Wait;

        Obj_Notice.SetActive(false);
    }
}
