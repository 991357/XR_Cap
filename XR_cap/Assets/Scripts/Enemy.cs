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
    public Collider2D C_Coll;
    public SpriteRenderer SR_SPriter;
    Animator A_Anim;
    WaitForFixedUpdate WFU_Wait;
    SpriteRenderer SR;
    //FreezeEnemy Freeze;

    public bool B_IsFlip;
    bool B_IsLive;
    //bool IsHit;
    public bool IsFreeze;
    bool IsBurn;
    bool IsBlazeWall;

    private void Awake()
    {
        R_Rigid = GetComponent<Rigidbody2D>();
        C_Coll = GetComponent<Collider2D>();
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return;

        if (!B_IsLive || A_Anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        if (IsFreeze)
            return;

        Vector2 V_DirVec = R_Target.position - R_Rigid.position;        //타겟위치 - 나의위치 = 방향(위치차이)
        Vector2 V_NextVec = V_DirVec.normalized * F_Speed * Time.fixedDeltaTime;

        if (GameManager.Instance.C_Manager.IsAction)
            return;
        R_Rigid.MovePosition(R_Rigid.position + V_NextVec);
        //R_Rigid.velocity = Vector2.zero;

        HitTimer += Time.fixedDeltaTime;

        if (IsBurn)
            Burning();
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
        C_Coll.enabled = true;
        R_Rigid.simulated = true;
        IsFreeze = false;
        SR_SPriter.sortingOrder = 2;
        A_Anim.SetBool("Dead", false);
        if (Name == "B_A_0")
            StartCoroutine(Walk());

        SR.enabled = false;
        F_Health = F_MaxHealth;
    }
    //public void Init(SpawnData data)
    //{
    //    A_Anim.runtimeAnimatorController = RAC_AnimCon[data.I_SpriteType];
    //    F_Speed = data.F_Speed;
    //    F_MaxHealth = data.I_Health;
    //    F_Health = data.I_Health;
    //}

    void Burning()
    {
        StartCoroutine(Burn());
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
                    if(F_Health < 0)
                        EnemyDead();
                    break;
                case 2:
                    F_Health -= 0.5f;
                    if (F_Health < 0)
                        EnemyDead();
                    break;
                case 3:
                    F_Health -= 0.7f;
                    if (F_Health < 0)
                        EnemyDead();
                    break;
                default:
                    F_Health -= 1;
                    if (F_Health < 0)
                        EnemyDead();
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !B_IsLive)
            return;

        if (collision.gameObject.tag == "Bullet")
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
                EnemyDead();
            }
        }

        if (collision.GetComponent<Bullet>().Name == "Slash" || collision.GetComponent<Bullet>().Name == "IceBall")
        {
            int ran = UnityEngine.Random.Range(0, 5);

            GameObject Par = GameManager.Instance.P_Manager.Get(26);
            Par.transform.position = transform.position;
            Par.transform.rotation = transform.rotation;

            if (ran == 0)
            {
                IsFreeze = true;

                StartCoroutine(EnemyFreeze());
                //int ran을 전역변수로 빼서 int ran이 0이 아닐때

                //이거 혹시 0인 상태로 얼었는데 바로 또 맞아서 1이 떴는데 1이 떠있는 상태로 코루틴이 돌고 있어서 안되는거 아닌지?
            }
            else
            {
                IsFreeze = false;
            }
        }
        else if (collision.GetComponent<Bullet>().Name == "FireBall" || collision.GetComponent<Bullet>().Name == "BlazeWall") //|| collision.GetComponent<Bullet>().Name == "FireBallSmall")
        {
            FireStack++;
            GameObject par = GameManager.Instance.P_Manager.Get(27);
            par.transform.position = transform.position;
            par.transform.rotation = transform.rotation;

            if (FireStack >= 3)
            {
                IsBurn = true;
            }
        }
    }

    void EnemyDead()
    {
        B_IsLive = false;
        C_Coll.enabled = false;
        R_Rigid.simulated = false;
        SR_SPriter.sortingOrder = 1;
        if (GameManager.Instance.Q_Manager.IsQuest)
            GameManager.Instance.Q_Manager.Count += 1;
        A_Anim.SetBool("Dead", true);

        if (Name == "A")
        {
            StartCoroutine(Dead(0.5f));
        }
        else if (Name == "B_A")
        {
            StartCoroutine(Dead(0.65f));
        }
        else if (Name == "C" || Name == "B_A_0")
        {
            StartCoroutine(Dead(0.6f));
        }
        else if (Name == "B" || Name == "B_B")
        {
            StartCoroutine(Dead(0.8f));
        }
        else if (Name == "B_C")
        {
            StartCoroutine(Dead(1f));
        }
        GameManager.Instance.Kill++;
        if (Name == "B_A" || Name == "B_B" || Name == "B_C")
        {
            for (int i = 0; i < 20; i++)
                GameManager.Instance.GetExp();
        }
        else
            GameManager.Instance.GetExp();

        if (GameManager.Instance.IsLive)
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Dead);
    }
    IEnumerator EnemyFreeze()
    {
        SR_SPriter.color = new Color(0, 0, 1);
        C_Coll.isTrigger = true;

        yield return new WaitForSeconds(3f);

        SR_SPriter.color = new Color(1, 1, 1);
        C_Coll.isTrigger = false;
        IsFreeze = false;
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
                else if (Name == "B_A_0")
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
                B_IsLive = false;
                C_Coll.enabled = false;
                R_Rigid.simulated = false;
                SR_SPriter.sortingOrder = 1;
                if (GameManager.Instance.Q_Manager.IsQuest)
                    GameManager.Instance.Q_Manager.Count += 1;
                A_Anim.SetBool("Dead", true);

                if (Name == "A" || Name == "B_A")
                {
                    StartCoroutine(Dead(0.65f));
                }
                else if (Name == "C")
                {
                    StartCoroutine(Dead(0.6f));
                }
                else if (Name == "B")
                {
                    StartCoroutine(Dead(0.8f));
                }
                else if (Name == "B_C")
                {
                    StartCoroutine(Dead(1.5f));
                }
                GameManager.Instance.Kill++;
                GameManager.Instance.GetExp();

                if (GameManager.Instance.IsLive)
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.Dead);
            }
        }

        Bullet bullet = collision.GetComponent<Bullet>();
        if(bullet != null && bullet.Name == "BlazeWall")
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

    IEnumerator Dead(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    //void ReturnSprite()
    //{
    //    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    //    sr.color = new Color(1, 1, 1, 1);
    //}
}
