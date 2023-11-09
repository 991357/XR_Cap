using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    [Header("GameInfo")]
    public Vector2 InputVec;
    Rigidbody2D Rigid;
    public SpriteRenderer SR;
    Animator Anim;
    CapsuleCollider2D Collider;
    public Scanner Scanner;
    public Item Item;
    public RuntimeAnimatorController[] RAC;
    public Camera Cam;
    public CamShake CamShake;
    public GameObject WindObj;

    [Space(10f)]
    [Header("Float")]
    public float Speed;
    public float DashCoolTime;
    public float DashTimer;
    public float QCoolTime;
    public float QTimer;
    float TornadoTimer;
    float Delay;

    [Space(10f)]
    [Header("Int")]
    public int Power;

    [Space(10f)]
    [Header("Bool")]
    public bool IsDash;

    [Space(10f)]
    [Header("ETC")]
    public GameObject ScanObj;
    public Vector3 DirVec;
    public GameObject SlowArea;

    [Space(10f)]
    [Header("List")]
    public List<Weapon> WeaponList = new List<Weapon>();

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        Collider = GetComponent<CapsuleCollider2D>();
        Scanner = GetComponent<Scanner>();
        //Debug.Log(GameManager.Instance.PlayerId);
    }

    private void OnEnable()
    {
        DashCoolTime = 2f;
        IsDash = false;
        DashTimer = 2f;

        //Debug.Log(GameManager.Instance.PlayerId);
        Anim.runtimeAnimatorController = RAC[GameManager.Instance.PlayerId];
        //Speed *= Character.Speed;
    }


    void Update()
    {
        if (!GameManager.Instance.IsLive)
            return;

        InputVec.x = GameManager.Instance.C_Manager.IsAction ? 0 : Input.GetAxisRaw("Horizontal");
        InputVec.y = GameManager.Instance.C_Manager.IsAction ? 0 : Input.GetAxisRaw("Vertical");

        if (InputVec.x == -1)
            DirVec = Vector3.left;
        else if (InputVec.x == 1)
            DirVec = Vector3.right;
        else if (InputVec.y == -1)
            DirVec = Vector3.down;
        else if (InputVec.y == 1)
            DirVec = Vector3.up;

        DashTimer += Time.deltaTime;
        QTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DashTimer > DashCoolTime)
            {
                //Dash 구현
                IsDash = true;
                DashTimer = 0;
                //Collider.isTrigger = true;
                Anim.SetTrigger("Dash");
                Speed = 17;
                Vector2 V_DashVec = InputVec.normalized * Speed * Time.fixedDeltaTime;
                Rigid.MovePosition(Rigid.position + V_DashVec);
                StartCoroutine(SpeedReset());
            }
            else
                return;
        }

        if (Power >= 3)
            Power = 3;
        if (GameManager.Instance.PlayerId == 0 || GameManager.Instance.PlayerId == 2)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (QTimer > QCoolTime)
                {
                    switch (Power)
                    {
                        case 1:
                            GameManager.Instance.P_Manager.Get(9);
                            QTimer = 0;
                            break;
                        case 2:
                            for (int i = 0; i < 10; i++)
                            {
                                GameManager.Instance.P_Manager.Get(10);
                            }
                            QTimer = 0;
                            break;
                        case 3:
                            //QCoolTime = 10f;
                            break;
                    }
                }

                else
                    return;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                if (QTimer > QCoolTime)
                {
                    switch (Power)
                    {
                        case 3:
                            TornadoTimer += Time.deltaTime;
                            
                            if (TornadoTimer > 2)
                            {
                                WindObj.SetActive(true);
                            }
                            break;
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                if (QTimer > QCoolTime)
                {
                    switch (Power)
                    {
                        case 3:
                            CamShake.IsShake = true;

                            GameManager.Instance.P_Manager.Get(11);
                            QTimer = 0;
                            TornadoTimer = 0;
                            WindObj.SetActive(false);
                            break;
                    }
                }
            }
        }
        else if (GameManager.Instance.PlayerId == 1)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (QTimer > QCoolTime)
                {
                    QTimer = 0;
                    switch (Power)
                    {
                        case 1:
                            GameManager.Instance.P_Manager.Get(13);
                            break;
                        case 2:
                            GameManager.Instance.P_Manager.Get(14);
                            break;
                        case 3:
                            //for (int i = 0; i < 30; i++)
                            //{
                                GameManager.Instance.P_Manager.Get(15);
                                //Invoke("Meteo", 1f);
                            //}

                            //Timer = Time.deltaTime;
                            //Delay = 0.01f;
                            //for (int i = 0; i < 15; i++)
                            //{
                            //    if (Timer < Delay)
                            //        return;
                            //    Timer = 0;
                            //    GameManager.Instance.S_Pool.Get(15);
                            //}
                            break;
                    }
                }
                else
                    return;
            }
        }
        else if (GameManager.Instance.PlayerId == 2)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (QTimer > QCoolTime)
                {
                    QTimer = 0;
                    switch (Power)
                    {
                        case 1:
                            //GameManager.Instance.S_Pool.Get(16);
                            break;
                        case 2:
                            //GameManager.Instance.S_Pool.Get(14);
                            break;
                        case 3:
                            //GameManager.Instance.S_Pool.Get(15);
                            break;
                    }
                }
                else
                    return;
            }
        }

        //Test Code
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponentInChildren<Weapon>().TestLevelUp(10, 10);
        }
        //Test Code
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.Obj_EnemyCleaner.SetActive(true);
            StartCoroutine(TurnOffEnemyCleaner());
        }
        //Test Code
        if (Input.GetKeyDown(KeyCode.C) && ScanObj != null)
        {
            GameManager.Instance.C_Manager.Action(ScanObj);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Power += 1;
            if (Power > 3)
                Power = 1;
        }

        //SlowArea Test Code
        if (GameManager.Instance.LevelUp.items[1].Level > 3)
            SlowArea.SetActive(true);
    }

    IEnumerator TurnOffEnemyCleaner()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.Obj_EnemyCleaner.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return;

        Vector2 V_NextVec = InputVec.normalized * Speed * Time.fixedDeltaTime;
        Rigid.MovePosition(Rigid.position + V_NextVec);

        Debug.DrawRay(Rigid.position, DirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D ray = Physics2D.Raycast(Rigid.position, DirVec, 0.7f, LayerMask.GetMask("NPC"));
        if (ray.collider != null)
            ScanObj = ray.collider.gameObject;
        else
            ScanObj = null;
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.IsLive)
            return;

        Anim.SetFloat("Speed", InputVec.magnitude); //벡터의 순수한 크기 값
        if (InputVec.x != 0)
        {
            SR.flipX = InputVec.x < 0;
        }
    }


    IEnumerator SpeedReset()
    {
        yield return new WaitForSeconds(0.2f);

        switch (Item.Level)
        {
            case 0:
                Speed = 5f;
                break;
            case 1:
                Speed = 6f;
                break;
            case 2:
                Speed = 7.2f;
                break;
            case 3:
                Speed = 6.220799f;
                break;
            case 4:
                Speed = 8.639999f;
                break;
            case 5:
                Speed = 10.63f;
                break;
            default:
                Speed = 3;
                break;
        }
        Collider.isTrigger = false;
        IsDash = false;
    }

    IEnumerator HitEnemy()
    {
        SR.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.1f);
        SR.color = new Color(1, 1, 1, 1);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.IsLive)
            return;
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.Health -= Time.deltaTime * 10;
            StartCoroutine(HitEnemy());
            if (GameManager.Instance.Health < 0)
            {
                for (int i = 2; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                Anim.SetTrigger("Dead");
                GameManager.Instance.GameOver();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            GameManager.Instance.Health -= 10;
            CapsuleCollider2D cap = collision.gameObject.GetComponent<CapsuleCollider2D>();
            StartCoroutine(HitEnemyBullet(cap));
            if (GameManager.Instance.Health < 0)
            {
                for (int i = 2; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                Anim.SetTrigger("Dead");
                GameManager.Instance.GameOver();
            }
        }
    }
    IEnumerator HitEnemyBullet(CapsuleCollider2D cap)
    {
        cap.isTrigger = true;
        SR.color = new Color(1, 0, 1, 1);
        yield return new WaitForSeconds(1);
        cap.isTrigger = false;
        SR.color = new Color(1, 1, 1, 1);
    }
}
