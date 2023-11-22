using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Name;

    public float F_Speed;
    public float F_Health;
    public float F_MaxHealth;
    float HitTimer;
    float HitDelay = 0.5f;
    float Timer;

    public int FireStack;

    public Rigidbody2D R_Target;
    public RuntimeAnimatorController[] RAC_AnimCon;     //더 많은 몬스터를 넣고 싶다면 여기에 애니메이터를 추가해서 넣어주세요!

    Rigidbody2D R_Rigid;
    public CapsuleCollider2D C_Coll;
    public SpriteRenderer SR_SPriter;
    Animator A_Anim;
    WaitForFixedUpdate WFU_Wait;
    SpriteRenderer SR;
    //FreezeEnemy Freeze;

    public bool B_IsFlip;
    bool B_IsLive = true;
    //bool IsHit;
    public bool IsFreeze;
    bool IsBurn;
    bool IsSlowBurn;
    bool IsBlazeWall;
    bool IsNet;
    bool IsDead;

    private void Awake()
    {
        R_Rigid = GetComponent<Rigidbody2D>();
        C_Coll = GetComponent<CapsuleCollider2D>();
        SR_SPriter = GetComponent<SpriteRenderer>();
        A_Anim = GetComponent<Animator>();
        WFU_Wait = new WaitForFixedUpdate();
        SR = GetComponent<SpriteRenderer>();
        //Freeze = GetComponent<FreezeEnemy>();
        B_IsFlip = true;
    }

    private void Update()
    {
        if (IsBlazeWall)
            StayBlazeWall();

        if (IsFreeze)
            StartCoroutine(EnemyFreeze());

        EnemyDeadCheck();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return;

        if (!B_IsLive || A_Anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        if (IsFreeze)
            StartCoroutine(EnemyFreeze());

        if (!IsFreeze)
        {

            Vector2 V_DirVec = R_Target.position - R_Rigid.position;        //타겟위치 - 나의위치 = 방향(위치차이)
            Vector2 V_NextVec = V_DirVec.normalized * F_Speed * Time.fixedDeltaTime;

            if (GameManager.Instance.C_Manager.IsAction)
                return;
            R_Rigid.MovePosition(R_Rigid.position + V_NextVec);
            //R_Rigid.velocity = Vector2.zero;
        }

        HitTimer += Time.fixedDeltaTime;

        if (IsBurn)
            StartCoroutine(Burn());

        if (IsSlowBurn)
            StartCoroutine(SlowBurn());     //Net맞으면
    }
    private void LateUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return;
        if (!B_IsLive)
            return;

        // if (RAC_AnimCon.)
        // {
        //     SR_SPriter.flipX = R_Target.position.x < R_Rigid.position.x;
        // }
        //else
        //{
        SR_SPriter.flipX = R_Target.position.x > R_Rigid.position.x;
        //}
    }

    private void OnEnable()
    {
        R_Target = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
        B_IsLive = true;
        SR_SPriter.color = new Color(1, 1, 1);

        R_Rigid.simulated = true;
        IsFreeze = false;
        SR_SPriter.sortingOrder = 2;
        A_Anim.SetBool("Dead", false);
        if (Name == "B_A")
            StartCoroutine(Walk());

        SR.enabled = false;
        F_Health = F_MaxHealth;
    }
    IEnumerator Burn()
    {
        //파티클 생성


        if (HitTimer > HitDelay)
        {
            switch (GameManager.Instance.LevelUp.items[1].Level)
            {
                case 1:
                    F_Health -= 0.3f;
                    if (F_Health < 0)
                        A_Anim.SetBool("Dead", true);
                    break;
                case 2:
                    F_Health -= 0.5f;
                    if (F_Health < 0)
                        A_Anim.SetBool("Dead", true);
                    break;
                case 3:
                    F_Health -= 0.7f;
                    if (F_Health < 0)
                        A_Anim.SetBool("Dead", true);
                    break;
                default:
                    F_Health -= 1;
                    if (F_Health < 0)
                        A_Anim.SetBool("Dead", true);
                    break;
            }
            HitTimer = 0;
        }

        //피격 표시   ?? 붙탄 파티클이 나오고 있으면 뭐 0.5초마다 빨갛게-> 원래대로 빨갛게 -> 원래대로

        //파티클 끄기

        yield return new WaitForSeconds(5);
        IsBurn = false;
    }


    IEnumerator Walk()
    {
        yield return new WaitForSeconds(0.7f);
        A_Anim.SetTrigger("IsWalk");
    }

    void EnemyDeadCheck()
    {
        if (A_Anim.GetCurrentAnimatorStateInfo(0).IsName("Dead") == true)
        {
            // 원하는 애니메이션이라면 플레이 중인지 체크
            float animTime = A_Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime > 0 && animTime < 1.0f)
            {

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !B_IsLive)
            return;

        if (collision.gameObject.tag == "Bullet")
        {
            // Test
            if (B_IsLive)
            {
                F_Health -= collision.GetComponent<Bullet>().F_Dmg;

                StartCoroutine(KnockBack());

                if (F_Health > 0)
                {
                    if (Name == "A")
                    {
                        A_Anim.SetTrigger("Hit");
                    }
                    else
                    {
                        //수현님한테 Enemy Animation 만들어달라 하기

                        //if (IsFreeze)
                        //    return;
                        ////RGB값 처리 예정
                        //SpriteRenderer sr = GetComponent<SpriteRenderer>();
                        //sr.color = new Color(1, 0, 0, 1);
                        //Invoke("ReturnSprite", 0.15f);
                    }
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
                }
                else
                {
                    A_Anim.SetBool("Dead", true);
                    B_IsLive = false;

                    if (Name == "A" || Name == "B" || Name == "C" || Name == "B_A")
                        GameManager.Instance.Kill++;
                    else
                    {
                        GameObject box = GameManager.Instance.P_Manager.Get(32);
                        box.transform.position = transform.position;
                        GameManager.Instance.BossKillCount++;
                    }
                }
            }
        }

        if (collision.GetComponent<Bullet>().Name == "Slash" || collision.GetComponent<Bullet>().Name == "IceBall")
        {
            int ran = UnityEngine.Random.Range(0, 5);

            GameObject Par = GameManager.Instance.P_Manager.Get(26);
            Par.transform.position = transform.position;
            Par.transform.rotation = transform.rotation;

            StartCoroutine(DestroyPar(Par));
            if (ran == 0)
            {
                IsFreeze = true;
            }
            else
            {
                IsFreeze = false;
            }
        }

        if (collision.GetComponent<Bullet>().Name == "FireBall" || collision.GetComponent<Bullet>().Name == "BlazeWall") //|| collision.GetComponent<Bullet>().Name == "FireBallSmall")
        {
            FireStack++;
            GameObject par = GameManager.Instance.P_Manager.Get(27);
            par.transform.position = transform.position;
            par.transform.rotation = transform.rotation;

            StartCoroutine(DestroyPar(par));
            if (FireStack >= 3)
            {
                IsBurn = true;
            }
        }

        if (collision.GetComponent<Bullet>().Name == "SlowNet")
        {
            Debug.Log("SlowNet");
            switch (GameManager.Instance.LevelUp.items[9].Level)        //나중에 바꾸기
            {
                case 1:
                    StartCoroutine(EnemySlow());
                    break;
                case 2:
                    StartCoroutine(EnemySlow());
                    break;

                case 3:
                    //상태이상
                    break;
                case 4:
                    //자폭병
                    break;
                default:
                    break;
            }
        }

        if (collision.GetComponent<Bullet>().Name == "SlowNet_2")
        {
            if (IsNet)
                return;

            //랜덤상태이상 부여
            int ran = UnityEngine.Random.Range(0, 4);

            switch (ran)
            {
                case 0:
                    IsNet = true;
                    IsFreeze = true;
                    break;
                case 1:
                    IsNet = true;
                    IsSlowBurn = true;
                    break;
                case 2:
                    //감전
                    IsNet = true;
                    Debug.Log("감전");
                    break;
                case 3:
                    //출혈
                    IsNet = true;
                    Debug.Log("출혈");
                    break;
            }
        }

        if (collision.GetComponent<Bullet>().Name == "Lightning")
        {

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "OC_Area")
            SR.enabled = true;

        if (collision.gameObject.tag == "Fire")
        {
            F_Health -= collision.GetComponent<Bullet>().F_Dmg;
            StartCoroutine(KnockBack());

            if (F_Health > 0)
            {
                if (Name == "A")
                {
                    A_Anim.SetTrigger("Hit");
                }
                else if (Name == "B_A")
                {
                    Debug.Log("애기가 맞음");
                }
                else
                {
                    ////RGB값 처리 예정
                    //SpriteRenderer sr = GetComponent<SpriteRenderer>();
                    //sr.color = new Color(1, 0, 0, 1);
                    //Invoke("ReturnSprite", 0.15f);
                }

                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
            }
            else
            {
                GameManager.Instance.Kill++;
                GameManager.Instance.GetExp(1);

                if (GameManager.Instance.IsLive)
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.Dead);
            }
        }

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && bullet.Name == "BlazeWall")
            IsBlazeWall = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.name == "OC_Area")
            SR.enabled = false;

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && bullet.Name == "BlazeWall")
            IsBlazeWall = false;
    }

    IEnumerator EnemySlow()
    {
        switch (Name)
        {
            case "A":
                F_Speed = 0.8f;
                break;
            case "B":
                F_Speed = 1f;
                break;
            case "C":
                F_Speed = 0.8f;
                break;
            case "BossA":
                F_Speed = 0.5f;
                break;
            case "B_A":
                F_Speed = 2f;
                break;
            case "BossB":
                F_Speed = 0.8f;
                break;
            case "BossC":
                F_Speed = 1f;
                break;
        }
        yield return new WaitForSeconds(3);

        switch (Name)
        {
            case "A":
                F_Speed = 2f;
                break;
            case "B":
                F_Speed = 2.2f;
                break;
            case "C":
                F_Speed = 2.5f;
                break;
            case "BossA":
                F_Speed = 1.2f;
                break;
            case "B_A":
                F_Speed = 3.5f;
                break;
            case "BossB":
                F_Speed = 2f;
                break;
            case "BossC":
                F_Speed = 2.3f;
                break;
        }
    }

    IEnumerator SlowBurn()
    {
        //파티클 생성

        if (HitTimer > HitDelay)
        {
            F_Health -= 0.3f;
            if (F_Health < 0)
                A_Anim.SetBool("Dead", true);

            HitTimer = 0;
        }

        yield return new WaitForSeconds(3);

        //파티클 끄기

        IsSlowBurn = false;
    }

    IEnumerator DestroyPar(GameObject Par)
    {
        yield return new WaitForSeconds(0.3f);
        Par.gameObject.SetActive(false);
    }


    public IEnumerator EnemyFreeze()
    {
        C_Coll.isTrigger = true;
        R_Rigid.velocity = Vector2.zero;
        SR_SPriter.color = new Color(0, 0, 1);

        yield return new WaitForSeconds(3f);

        SR_SPriter.color = new Color(1, 1, 1);
        C_Coll.isTrigger = false;
        IsFreeze = false;
    }

    void StayBlazeWall()
    {
        Timer += Time.deltaTime;
        if (Timer > 0.5f)
        {
            switch (GameManager.Instance.LevelUp.items[5].Level)
            {
                case 1:
                    F_Health -= 0.2f;
                    break;
                case 2:
                    F_Health -= 0.3f;
                    break;
                case 3:
                    F_Health -= 0.4f;
                    break;
                case 4:
                    F_Health -= 0.5f;
                    break;
                default:
                    break;
            }
            Timer = 0;
        }
    }

    IEnumerator KnockBack()
    {
        //yield return null;                          //1프레임 쉬기 
        //yield return new WaitForSeconds(2f);        //2초 쉬기
        yield return WFU_Wait;                      //다음 하나의 물리 프레임 딜레이
        Vector3 playerpos = GameManager.Instance.Player.transform.position;
        Vector3 dirvec = transform.position - playerpos;

        R_Rigid.AddForce(dirvec.normalized * 1f, ForceMode2D.Impulse);
    }

    public void EnemyDead()
    {
        //R_Rigid.simulated = false;
        //SR_SPriter.sortingOrder = 1;

        if (this.Name == "BossA")
        {
            Debug.Log("죽음");
        }

        if (GameManager.Instance.Q_Manager.IsQuest)
            GameManager.Instance.Q_Manager.Count += 1;

        GameManager.Instance.GetExp(1);

        if (GameManager.Instance.IsLive)
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Dead);

        this.gameObject.SetActive(false);

    }
}
