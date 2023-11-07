using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string Name;

    public float F_Dmg;
    public int I_Per;

    public bool IsRotate;
    bool IsTouch;

    public Vector3 Dir;
    Rigidbody2D R_Rigid;

    private void Awake()
    {
        R_Rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if(Name == "Spear")
            transform.localScale = new Vector3(1, 1, 1);

        if(Name == "Fire2")
        {
            //Sin 그래프로
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            Vector2 dir = new Vector2(Mathf.Sin(Mathf.PI * 2 * 2 / 10),-1);
            rigid.AddForce(dir.normalized * 14, ForceMode2D.Impulse);

            //1107 여기까지 했음
        }
    }

    private void Update()
    {
        Dead();

        if(IsRotate)
            transform.Rotate(Vector3.forward * 0.5f);
    }

    public void Init(float dmg, int per, Vector3 dir)
    {
        F_Dmg = dmg;
        I_Per = per;

        if(per >= 0)
        {
            if (Name == "Fire2")
                return;
            switch (GameManager.Instance.LevelUp.items[1].Level)
            {
                case 1:
                    R_Rigid.velocity = dir * 14f;
                    break;
                case 2:
                    R_Rigid.velocity = dir * 14f;
                    break;
                case 3:
                    R_Rigid.velocity = dir * 14f;
                    break;
                case 4:
                    R_Rigid.velocity = dir * 16f;
                    break;
                case 5:
                    R_Rigid.velocity = dir * 17f;
                    break;
                default:
                    R_Rigid.velocity = dir * 18f;
                    break;
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || I_Per == -100 || IsTouch)
            return;

        I_Per--;

        if (GameManager.Instance.LevelUp.items[1].Level > 3)
        {
            int rannum = Random.Range(10, 13);
            for (int i = 0; i < rannum; i++)
            {
                GameObject test = GameManager.Instance.P_Manager.Get(22);
                //test.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                test.transform.position = transform.position;
                //test.transform.rotation = Quaternion.FromToRotation(Vector3.right * 0.1f, dir);

                Rigidbody2D rigid = test.GetComponent<Rigidbody2D>();
                SubBullet bulletlogic = test.GetComponent<SubBullet>();
                bulletlogic.F_Dmg = 1;
                if (GameManager.Instance.LevelUp.items[1].Level > 4)
                    bulletlogic.I_Per = 99;

                //bulletlogic.IsRotate = true;

                Vector2 dirvec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / rannum), Mathf.Sin(Mathf.PI * 2 * i / rannum));

                if(GameManager.Instance.LevelUp.items[1].Level > 4)
                    rigid.AddForce(dirvec.normalized * 20, ForceMode2D.Impulse);
                else
                    rigid.AddForce(dirvec.normalized * 16, ForceMode2D.Impulse);

                Vector3 rotvec = Vector3.forward * 360 * i / 10 + Vector3.forward * 90;
                test.transform.Rotate(rotvec);
            }
        }

        if(GameManager.Instance.LevelUp.items[0].Level > 3)
        {
            if(Name == "Spear")
                StartCoroutine(SpearRotate());
        }

        if (I_Per < 0)
        {
            R_Rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    IEnumerator SpearRotate()
    {
        yield return new WaitForSeconds(0.1f);
        R_Rigid.velocity = Vector2.zero;

        if(Name == "Spear")
            transform.localScale = new Vector3(0.1f,0.1f,0.1f);

        IsTouch = true;

        yield return null;
        GameObject Pillar = GameManager.Instance.P_Manager.Get(24);
        Pillar.transform.position = transform.position;
        Pillar.GetComponent<SubBullet>().F_Dmg = 10;
        Pillar.GetComponent<SubBullet>().I_Per = 999;

        if(GameManager.Instance.LevelUp.items[0].Level > 4)
        {
            yield return new WaitForSeconds(0.2f);

            GameObject sword = GameManager.Instance.P_Manager.Get(25);
            sword.GetComponent<Bullet>().I_Per = 99;
            sword.GetComponent<Bullet>().F_Dmg = 10;
        }
        else


        yield return new WaitForSeconds(1f);
        IsTouch = false;
        Pillar.SetActive(false);

        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }


    void Dead()
    {
        Transform target = GameManager.Instance.Player.transform;
        Vector3 targetPos = target.position;
        float dir = Vector3.Distance(targetPos, transform.position);
        if (dir > 20f)
            this.gameObject.SetActive(false);
    }
}
