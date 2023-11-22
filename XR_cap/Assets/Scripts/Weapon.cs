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
    public int WeaponCount;

    public float F_Dmg;
    public float F_Speed;       //회전 속도

    public float SpearShotDealy = 1;

    float F_Timer;
    float F_Timer1;
    float F_Timer2;
    float F_Timer3;
    float F_Timer4;
    float F_Timer5;

    Player S_Player;
    public bool IsWeapon0;
    public bool IsWeapon1;
    public bool IsReady = true;

    GameObject[] swords;

    private void Awake()
    {
        S_Player = GameManager.Instance.Player;
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);

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
                F_Timer += Time.deltaTime;

                if (F_Timer > SpearShotDealy)
                {
                    F_Timer = 0;
                    Spear();
                }
                break;

            case 1:
                transform.Rotate(Vector3.back * F_Speed * Time.deltaTime);

                if (GameManager.Instance.LevelUp.items[1].Level > 2)
                    F_Speed = 250;
                break;
            case 2:
                F_Timer1 += Time.deltaTime;

                if (F_Timer1 > F_Speed)
                {
                    F_Timer1 = 0;
                    Fire();
                }
                break;
            case 5:
                F_Timer2 += Time.deltaTime;

                if (F_Timer2 > F_Speed)
                {
                    F_Timer2 = 0;
                    BlazeWall();
                }
                break;
            case 6:
                F_Timer3 += Time.deltaTime;

                if (IsReady)
                {
                    if (F_Timer3 > F_Speed)
                    {
                        F_Timer3 = 0;
                        ShotLightning();
                    }
                }
                break;
            case 8:
                //transform.Rotate(Vector3.back * F_Speed * Time.deltaTime);

                break;
            case 9:
                F_Timer4 += Time.deltaTime;

                if (F_Timer4 > F_Speed)
                {
                    F_Timer4 = 0;
                    Slownet();
                }
                break;
            default:
                break;
        }
    }

    public void LevelUp(float dmg, int count)
    {
        F_Dmg = dmg;
        I_Count += count;

        GameManager.Instance.P_Manager.Get(3);
        StartCoroutine(TurnOffLevelUp());

        if (I_Id == 0)
            Batch();

        if (I_Id == 1)
            IceRotate();

        if (I_Id == 8)
            MesBatch();

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
                F_Speed = 300f;
                IceRotate();
                break;
            default:
                break;
        }
        GameManager.Instance.P_Manager.Get(3);
        StartCoroutine(TurnOffLevelUp());

        if (I_Id == 0)
            Batch();

        if (I_Id == 1)
            IceRotate();

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
            if (data.PB_Projectile == GameManager.Instance.P_Manager.Obj_Prefabs[i])
            {
                I_PrefabId = i;
                break;
            }
        }

        switch (I_Id)
        {
            case 0:                                         //얼음무기 1 (얼음 칼)
                IsWeapon0 = true;
                F_Speed = 150 * Character.WeaponSpeed;
                Batch();
                WeaponCount++;
                break;
            case 1:                                         //얼음무기 2 (회전하는 고드름)
                IsWeapon1 = true;
                //F_Speed = 1.2f * Character.WeaponRate;
                F_Speed = 150 * Character.WeaponSpeed;
                IceRotate();
                WeaponCount++;
                break;
            case 2:                                         //불 무기 1 (파이어볼)
                F_Speed = 1.2f * Character.WeaponRate;      //Shot Dealy
                WeaponCount++;
                break;
            case 5:                                         //불 무기 2 (화염장판)
                F_Speed = 1.5f;
                WeaponCount++;
                break;
            case 6:                                         //전기 무기 1 (체인라이트닝)
                F_Speed = 1.2f;
                WeaponCount++;
                break;
            case 7:                                         //전기 무기 2 (레일건)
                WeaponCount++;
                break;
            case 8:                                         //메스
                F_Speed = 150;
                MesBatch();
                WeaponCount++;
                break;
            case 9:                                         //Shot Dealy//슬로우그물
                F_Speed = 1.2f * Character.WeaponRate;
                WeaponCount++;
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
                bullet = GameManager.Instance.P_Manager.Get(1).transform;
                bullet.parent = transform;
            }

            bullet.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotvec = Vector3.forward * 360 * i / I_Count;
            bullet.Rotate(rotvec);
            //bullet.Translate(bullet.up * 2.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(F_Dmg, -100, Vector3.zero);      //-100 is Infinity per (관통)

            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Melee);
        }
    }

    IEnumerator TurnOffLevelUp()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject levelup = GameObject.FindGameObjectWithTag("LevelUp");
        levelup.SetActive(false);
    }

    void Spear()
    {
        if (!S_Player.Scanner.F_NearstTarget)
            return;

        Vector3 targetpos = S_Player.Scanner.F_NearstTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        switch (GameManager.Instance.LevelUp.items[0].Level)
        {
            case 3:
            case 4:
                Transform bullet = GameManager.Instance.P_Manager.Get(23).transform;
                bullet.position = transform.position;
                bullet.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);        //지정된 축을 중심으로 목표를 향해 회전하는 함수
                bullet.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);
                bullet.GetComponent<Bullet>().Dir = dir;
                break;
        }
    }

    void IceRotate()
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
                bullet = GameManager.Instance.P_Manager.Get(25).transform;
                bullet.parent = transform;
            }

            bullet.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotvec = Vector3.forward * 360 * i / I_Count;
            bullet.Rotate(rotvec);
            bullet.Translate(bullet.up * 2.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(F_Dmg, -100, Vector3.zero);      //-100 is Infinity per (관통)

            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Melee);
        }
    }

    void MesBatch()
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
                bullet = GameManager.Instance.P_Manager.Get(29).transform;
                bullet.parent = transform;
            }

            bullet.localScale = new Vector3(0.3f, 1f, 1f);
            bullet.localPosition = transform.position;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotvec = Vector3.forward * 360 * i / I_Count;
            bullet.Rotate(rotvec);
            bullet.Translate(bullet.up * 2f, Space.World);
            bullet.GetComponent<Bullet>().Init(F_Dmg, -100, Vector3.zero);      //-100 is Infinity per (관통)
            bullet.GetComponent<Mes>().Number = i;

            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Melee);
        }
    }

    void Fire()
    {
        if (!S_Player.Scanner.F_NearstTarget)
            return;

        Vector3 targetpos = S_Player.Scanner.F_NearstTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        switch (GameManager.Instance.LevelUp.items[2].Level)
        {
            case 1:
                Transform bullet = GameManager.Instance.P_Manager.Get(2).transform;
                bullet.position = transform.position;
                bullet.rotation = Quaternion.FromToRotation(Vector3.right, dir);        //지정된 축을 중심으로 목표를 향해 회전하는 함수
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

                bulletr.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);                     //-1 is Infinity per (관통)
                bulletl.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);
                break;
            default:
                Transform bulletrr = GameManager.Instance.P_Manager.Get(2).transform;
                Transform bulletll = GameManager.Instance.P_Manager.Get(2).transform;
                Transform bulletcc = GameManager.Instance.P_Manager.Get(2).transform;

                bulletcc.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                bulletll.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                bulletrr.localScale = new Vector3(0.6f, 0.6f, 0.6f);

                bulletcc.position = transform.position;
                bulletll.position = transform.position + Vector3.left * 0.7f;
                bulletrr.position = transform.position + Vector3.right * 0.7f;

                bulletrr.rotation = Quaternion.FromToRotation(Vector3.right * 0.3f, dir);        //지정된 축을 중심으로 목표를 향해 회전하는 함수
                bulletll.rotation = Quaternion.FromToRotation(Vector3.left * 0.3f, dir);
                bulletcc.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);

                bulletrr.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);                     //-1 is Infinity per (관통)
                bulletll.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);
                bulletcc.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);

                break;
        }
        //Transform bullet = GameManager.Instance.S_Pool.Get(I_PrefabId).transform;

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);
    }

    void ShotLightning()
    {
        GameObject lightning = GameManager.Instance.P_Manager.Get(33);

        lightning.transform.position = transform.position;
        lightning.transform.rotation = transform.rotation;

        lightning.GetComponent<Bullet>().Init(F_Dmg, -100, Vector3.up);
        IsReady = false;
    }

    void Slownet()
    {
        if (!S_Player.Scanner.F_NearstTarget)
            return;

        Vector3 targetpos = S_Player.Scanner.F_NearstTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        switch (GameManager.Instance.LevelUp.items[9].Level)        //나중에 바꾸기 
        {
            case 1:
                Transform bullet = GameManager.Instance.P_Manager.Get(30).transform;
                bullet.position = transform.position;
                bullet.rotation = Quaternion.FromToRotation(Vector3.right, dir);        //지정된 축을 중심으로 목표를 향해 회전하는 함수
                                                                                        //bullet.transform.LookAt(targetpos);
                bullet.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);                       //-1 is Infinity per (관통)
                break;
            default:
                Transform bulletr = GameManager.Instance.P_Manager.Get(30).transform;
                Transform bulletl = GameManager.Instance.P_Manager.Get(30).transform;
                bulletr.position = transform.position;
                bulletl.position = transform.position + Vector3.left * 0.7f;
                bulletr.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);        //지정된 축을 중심으로 목표를 향해 회전하는 함수
                bulletl.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);

                bulletr.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);                     //-1 is Infinity per (관통)
                bulletl.GetComponent<Bullet>().Init(F_Dmg, I_Count, dir);
                break;
        }
    }
    void BlazeWall()
    {
        switch (GameManager.Instance.LevelUp.items[5].Level)
        {
            case 1:
                int ranx = Random.Range(-5, 5);
                int rany = Random.Range(-4, 4);
                Transform bullet = GameManager.Instance.P_Manager.Get(28).transform;
                bullet.position = new Vector2(transform.position.x + ranx, transform.position.y + rany);
                break;
            case 2:
            case 3:
            case 4:
                int ranx1 = Random.Range(-5, 5);
                int rany1 = Random.Range(-4, 4);
                int ranx2 = Random.Range(-5, 5);
                int rany2 = Random.Range(-4, 4);
                Transform bulletr = GameManager.Instance.P_Manager.Get(28).transform;
                Transform bulletl = GameManager.Instance.P_Manager.Get(28).transform;
                bulletr.position = new Vector2(transform.position.x + ranx1, transform.position.y + rany1);
                bulletl.position = new Vector2(transform.position.x + ranx2, transform.position.y + rany2);
                break;
        }
    }
}