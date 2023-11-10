using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mes : MonoBehaviour
{
    public GameObject HitPoint;
    GameObject ScanObj;
    bool IsAttack;
    Animator Anim;

    // Start is called before the first frame update
    void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAttack)
            return;

        Vector3 dir = HitPoint.transform.position - transform.position;

        Debug.DrawRay(HitPoint.transform.position, dir.normalized * 1f, new Color(1, 0, 0));
        RaycastHit2D ray = Physics2D.Raycast(HitPoint.transform.position, dir, 1, LayerMask.GetMask("Enemy"));

        if (ray.collider != null)
        {
            ScanObj = ray.collider.gameObject;
            Debug.Log(ScanObj.name);
            Anim.SetTrigger("Attack");

            //StartCoroutine(Attack());
        }
        else
        {
            ScanObj = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //애니메이션 실행

            //근데 지금 애니메이션 진행중이야?

            //그럼 return

            //아니면 실행
        }
    }
}
