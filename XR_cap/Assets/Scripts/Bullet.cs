using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string Name;

    public float F_Dmg;
    float move_speed;
    float move_x_rate;
    float move_y_rate;

    public int I_Per;

    public bool IsRotate;
    bool IsTouch;
    bool IsJang;

    public Vector3 Dir;
    Rigidbody2D R_Rigid;

    private void Awake()
    {
        R_Rigid = GetComponent<Rigidbody2D>();
        //startTime = Time.time;
    }

    private void OnEnable()
    {
        if(Name == "Spear")
            transform.localScale = new Vector3(1, 1, 1);

        transform.rotation = Quaternion.identity;
    }

    private void Start()
    {
        if (Name == "Lightning")
        {
            move_speed = 15.0f;
            move_x_rate = Random.Range(-1.0f, 1.0f);
            move_y_rate = Random.Range(-1.0f, 1.0f);

            while (Mathf.Abs(move_x_rate) < 0.3f)
            {
                move_x_rate = Random.Range(-1.0f, 1.0f);
            }

            while (Mathf.Abs(move_y_rate) < 0.3f)
            {
                move_y_rate = Random.Range(-1.0f, 1.0f);
            }
        }
    }

    private void Update()
    {
        Dead();

        if(IsRotate)
            transform.Rotate(Vector3.forward * 0.5f);

        if(Name == "Lightning")
        {
            Lightning();
        }
        //MoveSin();
    }

    public void Init(float dmg, int per, Vector3 dir)
    {
        F_Dmg = dmg;
        I_Per = per;

        if (per >= 0)
        {
            switch (GameManager.Instance.LevelUp.items[2].Level)
            {
                case 1:
                    R_Rigid.velocity = dir * 14f;
                    break;
                case 2:
                    R_Rigid.velocity = dir * 14f;
                    break;
                case 3:
                    R_Rigid.velocity = dir * 16f;
                    break;
                case 4:
                    R_Rigid.velocity = dir * 16f;
                    break;
                default:
                    R_Rigid.velocity = dir * 18f;
                    break;
            }
        }

        if (per >= 0)
        {
            switch (GameManager.Instance.LevelUp.items[7].Level)        //나중에 바꾸기
            {
                case 1:
                    R_Rigid.velocity = dir * 10f;
                    break;
                case 2:
                    R_Rigid.velocity = dir * 10f;
                    break;
                case 3:
                    R_Rigid.velocity = dir * 10f;
                    break;
                case 4:
                    R_Rigid.velocity = dir * 10f;
                    break;
                default:
                    R_Rigid.velocity = dir * 18f;
                    break;
            }
        }
    }

    void Lightning()
    {
        transform.Translate(Vector3.right * Time.deltaTime * move_speed * move_x_rate, Space.World);
        transform.Translate(Vector3.up * Time.deltaTime * move_speed * move_y_rate, Space.World);

        // 카메라를 벗어나지 않도록 범위 제한
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        if (position.x < 0f)
        {
            position.x = 0f;
            move_x_rate = Random.Range(0.3f, 1.0f);
        }
        if (position.y < 0f)
        {
            position.y = 0f;
            move_y_rate = Random.Range(0.3f, 1.0f);
        }
        if (position.x > 1f)
        {
            position.x = 1f;
            move_x_rate = Random.Range(-1.0f, -0.3f);
        }
        if (position.y > 1f)
        {
            position.y = 1f;
            move_y_rate = Random.Range(-1.0f, -0.3f);
        }
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || I_Per == -100 || IsTouch)
            return;

        I_Per--;

        if (Name == "FireBall")
        {
            if (GameManager.Instance.LevelUp.items[2].Level > 3)
            {
                int rannum = Random.Range(10, 13);
                for (int i = 0; i < rannum; i++)
                {
                    GameObject test = GameManager.Instance.P_Manager.Get(22);
                    //test.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    test.transform.position = transform.position;
                    //test.transform.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);

                    Rigidbody2D rigid = test.GetComponent<Rigidbody2D>();
                    Bullet bulletlogic = test.GetComponent<Bullet>();
                    bulletlogic.F_Dmg = 0.5f;
                    bulletlogic.I_Per = 99;

                    Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / rannum), Mathf.Sin(Mathf.PI * 2 * i / rannum));

                    rigid.AddForce(dirvec.normalized * 16, ForceMode2D.Impulse);

                    Vector3 rotvec = Vector3.forward * 360 * i / 10 + Vector3.forward * 90;

                    test.transform.Rotate(rotvec);
                }
            }
        }

        if(Name == "Spear")
        {
            if (GameManager.Instance.LevelUp.items[0].Level > 3)
                StartCoroutine(IcePillar());
        }

        if (Name == "BlazeWall")
        {
            if (GameManager.Instance.LevelUp.items[5].Level > 2)
            {
                //폭발
            }
            if (GameManager.Instance.LevelUp.items[5].Level > 3)
            {
                //원형으로?
                
                //원형을 만든다
                //GameObject circle = GameManager.Instance.P_Manager.Get(29);
                //circle.transform.position = transform.position;
                //circle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                //
                ////퍼진다
                //for (int i = 0; i < 2; i++)
                //{
                //    circle.transform.localScale = new Vector3(transform.localScale.x + 0.000001f, transform.localScale.y + 0.000001f, transform.localScale.z + 0.000001f);
                //}
                //
                ////삭제한다
                //StartCoroutine(TurnOff(2,circle));
            }
        }

        if(Name == "Lightning")
        {

        }

        if(Name == "SlowNet")
        {
            if(GameManager.Instance.LevelUp.items[7].Level>2)//나중에바꾸기
            {
                if (IsJang)
                    return;
                //장판생성
                IsJang = true;
                GameObject jang = GameManager.Instance.P_Manager.Get(31);
                jang.transform.position = transform.position;
            }
        }

        if (I_Per < 0)
        {
            if (Name == "SlowNet_2")
                return;

            R_Rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    IEnumerator IcePillar()
    {
        R_Rigid.velocity = Vector2.zero;

        if(Name == "Spear")
            transform.localScale = new Vector3(0.1f,0.1f,0.1f);

        IsTouch = true;
        GameObject Pillar = GameManager.Instance.P_Manager.Get(24);
        Pillar.transform.position = transform.position;
        Pillar.GetComponent<Bullet>().F_Dmg = 10;
        Pillar.GetComponent<Bullet>().I_Per = 999;


        yield return new WaitForSeconds(1.5f);

        Pillar.gameObject.SetActive(false);
        IsTouch = false;
        gameObject.SetActive(false);
    }


    public void BulletRotate(Transform bullet)
    {
        bullet.Rotate(Vector3.back * 100 * Time.deltaTime);
    }

    void Dead()
    {
        if (Name == "Mes")
            return;

        Transform target = GameManager.Instance.Player.transform;
        Vector3 targetPos = target.position;
        float dir = Vector3.Distance(targetPos, transform.position);
        if (dir > 20f)
        {
            if (Name == "FireBall2" || Name == "FireBall3")
                Name = "FireBall";
            IsJang = false;
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator TurnOff(float time,GameObject circle)
    {
        yield return new WaitForSeconds(time);
        circle.gameObject.SetActive(false);
    }
}