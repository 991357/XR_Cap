using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mes : MonoBehaviour
{
     public GameObject HitPoint;
     GameObject ScanObj;
     bool IsAttack;
     Animator Anim;
     DOTweenAnimation DotAnim;
     bool isAnimationPlaying;

    public int Number;

    // Start is called before the first frame update
    void Awake()
    {
        Anim = GetComponent<Animator>();
        DotAnim = GetComponent<DOTweenAnimation>();
    }
   
    // Update is called once per frame
    void Update()
    {
        Vector3 dir = HitPoint.transform.position - transform.position;
        Debug.DrawRay(HitPoint.transform.position, dir.normalized * 1.5f, new Color(1, 0, 0));
        RaycastHit2D ray = Physics2D.Raycast(HitPoint.transform.position, dir, 1.5f, LayerMask.GetMask("Enemy"));
        
        if (ray.collider != null)
        {
            Test();
        }
        else
        {
            transform.position = transform.position;
        }
    }

    public void Test()
    {
        switch (GameManager.Instance.LevelUp.items[6].Level)        //나중에바꾸기
        {
            case 0:
                break;
            case 1:
                switch (Number)
                {
                    case 0:
                        Anim.SetTrigger("M0");
                        break;
                    case 1:
                        Anim.SetTrigger("M1");
                        break;
                }
                break;
            case 2:
                switch (Number)
                {
                    case 0:
                        Anim.SetTrigger("M0");
                        break;
                    case 1:
                        Anim.SetTrigger("M2");
                        break;
                    case 2:
                        Anim.SetTrigger("M1");
                        break;
                    case 3:
                        Anim.SetTrigger("M3");
                        break;
                }
                break;
            case 3:
                switch (Number)
                {
                    case 0:
                        Anim.SetTrigger("M0");
                        break;
                    case 1:
                        Anim.SetTrigger("M3-1");
                        break;
                    case 2:
                        Anim.SetTrigger("M3-2");
                        break;
                    case 3:
                        Anim.SetTrigger("M1");
                        break;
                    case 4:
                        Anim.SetTrigger("M3-4");
                        break;
                    case 5:
                        Anim.SetTrigger("M3-5");
                        break;
                }
                break;
            case 4:
                switch (Number)
                {
                    case 0:
                        Anim.SetTrigger("M0");
                        break;
                    case 1:
                        Anim.SetTrigger("M4-1");
                        break;
                    case 2:
                        Anim.SetTrigger("M2");
                        break;
                    case 3:
                        Anim.SetTrigger("M4-3");
                        break;
                    case 4:
                        Anim.SetTrigger("M1");
                        break;
                    case 5:
                        Anim.SetTrigger("M4-5");
                        break;
                    case 6:
                        Anim.SetTrigger("M3");
                        break;
                    case 7:
                        Anim.SetTrigger("M4-7");
                        break;
                }
                break;
        }
    }
}
