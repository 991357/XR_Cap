using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Pool Manager에서 받아온 것들을 장면안에서 관리하는 역할
    public int I_Id;
    public int I_PrefabId;
    public int I_Count;         //몇개나 배치할것인지?
    public int WeaponLevel;

    public float F_Dmg;
    public float F_Speed;       //회전 속도

    float F_Timer;

    Player S_Player;
    public bool IsWeapon0;
    public bool IsWeapon1;

    private void Awake()
    {
        S_Player = GameManager.Instance.PlayerLogic;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsLive)
            return;
        if (GameManager.Instance.C_Manager.IsAction)
            return;

        switch (I_Id)
        {
            case 0:
                transform.Rotate(Vector3.back * F_Speed * Time.deltaTime);
                break;

            case 1:
                F_Timer += Time.deltaTime;

                if(F_Timer > F_Speed)
                {
                    F_Timer = 0;
                    Fire();
                }
                break;
            default:
                break;
        }
    }

    public void LevelUp(float dmg , int count)
    {
        F_Dmg = dmg;
        I_Count += count;

       GameManager.Instance.P_Manager.Get(3);
        Invoke("TurnOffLevelUp", 0.5f);

        if (I_Id == 0)
            Batch();

        //S_Player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }

    public void TestLevelUp(float dmg, int count)
    {
        F_Dmg = dmg * Character.WeaponDmg;
        I_Count += count + Character.WeaponCount;

        switch (I_Id)
        {
            case 0:
                F_Speed = 300;
                Batch();
                break;
            case 1:
                F_Speed = 0.1f;
                break;
            default:
                break;
        }
        GameManager.Instance.P_Manager.Get(3);
        Invoke("TurnOffLevelUp", 0.5f);

        if (I_Id == 0)
            Batch();

    }
    public void Init(ItemData data)
    {
        name = "Weapon" + data.I_ItemId;
        transform.parent = S_Player.transform;
        transform.localPosition = Vector3.zero;

        I_Id = data.I_ItemId;
        F_Dmg = data.F_BaseDmg * Character.WeaponDmg;
        I_Count = data.I_BaseCount + Character.WeaponCount;

        for (int i = 0; i < GameManager.Instance.P_Manager.Obj_Prefabs.Length; i++)
        {
            if(data.PB_Projectile == GameManager.Instance.P_Manager.Obj_Prefabs[i])
            {
                I_PrefabId = i;
                break;
            }
        }

        switch(I_Id)
        {
            case 0:
                IsWeapon0 = true;
                F_Speed = 150 * Character.WeaponSpeed;
                Batch();
                break;
            case 1:
                IsWeapon1 = true;
                F_Speed = 1.2f * Character.WeaponRate;
                break;
            default:
                break;
        }

        //S_Player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for (int i = 0; i < I_Count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)               //원래는 ObjectPooling 에서만 가져왔던걸 이제는 내가 갖고있는 자식 오브젝트를 먼저 재활용하고 모자란걸 오브젝트 풀링으로 충당하겠다..!
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.Instance.P_Manager.Get(I_PrefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotvec = Vector3.forward * 360 * i / I_Count;
            bullet.Rotate(rotvec);
            bullet.Translate(bullet.up * 2.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(F_Dmg, -100,Vector3.zero);      //-100 is Infinity per (관통)

            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Melee);
        }
    }

    void TurnOffLevelUp()
    {
        GameObject levelup = GameObject.FindGameObjectWithTag("LevelUp");
        levelup.SetActive(false);
    }

   
    void Fire()
    {
        if (!S_Player.Scanner.F_NearstTarget)
            return;

        Vector3 targetpos = S_Player.Scanner.F_NearstTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        switch (GameManager.Instance.LevelUp.items[1].Level)
        {
            case 1:
                Transform bullet = GameManager.Instance.P_Manager.Get(2).transform;
                bullet.position = transform.position;
                bullet.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);        //지정된 축을 중심으로 목표를 향해 회전하는 함수
                                                                                               //bullet.transform.LookAt(targetpos);
                bullet.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);                       //-1 is Infinity per (관통)
                break;
            case 2:
                Transform bulletr = GameManager.Instance.P_Manager.Get(2).transform;
                Transform bulletl = GameManager.Instance.P_Manager.Get(2).transform;
                bulletr.position = transform.position;
                bulletl.position = transform.position + Vector3.left * 0.7f;
                bulletr.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);        //지정된 축을 중심으로 목표를 향해 회전하는 함수
                bulletl.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);    
                                                                                               //bullet.transform.LookAt(targetpos);
                bulletr.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);                     //-1 is Infinity per (관통)
                bulletl.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);
                break;
            default:
                Transform bulletrr = GameManager.Instance.P_Manager.Get(2).transform;
                Transform bulletll = GameManager.Instance.P_Manager.Get(2).transform;
                Transform bulletcc = GameManager.Instance.P_Manager.Get(2).transform;
            
                bulletcc.position = transform.position;
                bulletll.position = transform.position + Vector3.left * 0.7f;
                bulletrr.position = transform.position + Vector3.right * 0.7f;
            
                bulletrr.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);        //지정된 축을 중심으로 목표를 향해 회전하는 함수
                bulletll.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);
                bulletcc.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);
            
                bulletrr.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);                     //-1 is Infinity per (관통)
                bulletll.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);
                bulletcc.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);
                break;
            //case 4:
            //    for (int i = 0; i < 8; i++)
            //    {
            //        GameObject test = GameManager.Instance.P_Manager.Get(2);
            //        test.transform.position = transform.position;
            //        //test.transform.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);
            //
            //        Rigidbody2D rigid = test.GetComponent<Rigidbody2D>();
            //        Bullet bulletlogic = test.GetComponent<Bullet>();
            //        bulletlogic.I_Per = 999;
            //        bulletlogic.F_Dmg = 1;
            //        //bulletlogic.IsRotate = true;
            //
            //        Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 8), Mathf.Sin(Mathf.PI * 2 * i / 8));
            //
            //        rigid.AddForce(dirvec.normalized * 4, ForceMode2D.Impulse);
            //
            //        Vector3 rotvec = Vector3.forward * 360 * i / 8 + Vector3.forward;
            //        test.transform.Rotate(rotvec);
            //    }
            //    break;
            //default:
            //    for (int i = 0; i < 14; i++)
            //    {
            //        GameObject test = GameManager.Instance.P_Manager.Get(2);
            //        test.transform.position = transform.position;
            //        //test.transform.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);
            //
            //        Rigidbody2D rigid = test.GetComponent<Rigidbody2D>();
            //        Bullet bulletlogic = test.GetComponent<Bullet>();
            //        bulletlogic.F_Dmg = 1;
            //        //bulletlogic.IsRotate = true;
            //
            //        Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 14), Mathf.Sin(Mathf.PI * 2 * i / 14));
            //
            //        rigid.AddForce(dirvec.normalized * 6, ForceMode2D.Impulse);
            //
            //        Vector3 rotvec = Vector3.forward * 360 * i / 14 + Vector3.forward;
            //        test.transform.Rotate(rotvec);
            //    }
            //    break;
            //case 6:
            //    break;
            //default:
            //    Debug.Log("무기 레벨 초과");
            //    break;
        }
        //Transform bullet = GameManager.Instance.S_Pool.Get(I_PrefabId).transform;


        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);
    }

}