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
    public Notice Notice;

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
    public int UltimateCoin;

    [Space(10f)]
    [Header("Bool")]
    public bool IsDash;

    [Space(10f)]
    [Header("ETC")]
    public GameObject ScanObj;
    public Vector3 DirVec;
    public GameObject SlowArea;
    public GameObject FreezeArea;

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

    private void Start()
    {
        UltimateCoin = 0;
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

        //이동
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

        //대쉬
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

        if(Input.GetKeyDown(KeyCode.V))
        {
            Notice.IsAnim = true;
        }

        //최대 파워 설정
        if (Power >= 3)
            Power = 3;

        //파워 레벨업
        if (GameManager.Instance.Kill > 30)
            Power = 2;
        else if (GameManager.Instance.Kill > 100)
            Power = 3;

        //캐릭터별 스킬 설정
        if (GameManager.Instance.PlayerId == 0 || GameManager.Instance.PlayerId == 1)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (UltimateCoin == 1)
                {
                    switch (Power)
                    {
                        case 1:
                            StartCoroutine(FreezeAreaSkill());
                            break;
                        case 2:
                            StartCoroutine(FreezeAreaSkill());
                            break;
                        case 3:
                            StartCoroutine(FreezeAreaSkill());
                            break;
                    }
                    UltimateCoin--;
                }
                else
                    return;
            }
        }
        else if (GameManager.Instance.PlayerId == 2)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (UltimateCoin == 1)
                {
                    switch (Power)
                    {
                        case 1:
                            GameManager.Instance.P_Manager.Get(15);
                            break;
                        case 2:
                            GameManager.Instance.P_Manager.Get(15);
                            break;
                        case 3:
                            GameManager.Instance.P_Manager.Get(15);
                            break;
                    }
                    UltimateCoin--;
                }
                else
                    return;
            }
        }

        //Test Code
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.LevelUp.Show();
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
        {
            SlowArea.SetActive(true);
        }

        if(UltimateCoin == 1)
        {
            if(GameManager.Instance.PlayerId == 0 || GameManager.Instance.PlayerId == 1)
                GameManager.Instance.UltimateImg[0].SetActive(true);
            else
                GameManager.Instance.UltimateImg[1].SetActive(true);
        }
        else
        {
            if (GameManager.Instance.PlayerId == 0 || GameManager.Instance.PlayerId == 1)
                GameManager.Instance.UltimateImg[0].SetActive(false);
            else
                GameManager.Instance.UltimateImg[1].SetActive(false);
        }
    }

    IEnumerator FreezeAreaSkill()
    {
        FreezeArea.SetActive(true);
        yield return new WaitForSeconds(2);
        FreezeArea.SetActive(false);
        QTimer = 0;
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
        //대쉬 후 player speed를 장화 레벨에 따라 변경
        yield return new WaitForSeconds(0.2f);

        switch (GameManager.Instance.LevelUp.items[3].Level)
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

        //Enemy에게 닿으면
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
                Anim.SetBool("Dead", true);
                GameManager.Instance.GameOver();

            }
        }
    }

    public void GameOverSend()
    {
        GameManager.Instance.GameOverEnd();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //보스 스킬에 닿으면
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
                Anim.SetBool("Dead",true);
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
