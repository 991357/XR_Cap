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

    // Start is called before the first frame update
    void Awake()
    {
        Anim = GetComponent<Animator>();
        DotAnim = GetComponent<DOTweenAnimation>();
        transform.parent = GameManager.Instance.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAttack)
            return;

        if (isAnimationPlaying)
            return;

        Vector3 dir = HitPoint.transform.position - transform.position;

        Debug.DrawRay(HitPoint.transform.position, dir.normalized * 1f, new Color(1, 0, 0));
        RaycastHit2D ray = Physics2D.Raycast(HitPoint.transform.position, dir, 1, LayerMask.GetMask("Enemy"));

        if (ray.collider != null && ray.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // A 함수 호출
            A(ray.collider.gameObject);

            //1110 여기까지 했음--------------------------------------------------------------------------------------------------------------------------------------------------------
        }
        else
        {
            ScanObj = null;
        }
    }

    private void A(GameObject enemy)
    {
        // 애니메이션이 실행 중임을 플래그로 표시
        isAnimationPlaying = true;

        // DOTween을 사용하여 칼을 Enemy에게 이동시키고 되돌아오는 애니메이션 설정
        transform.DOMove(GameManager.Instance.Player.transform.position, 0.1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // 이동이 끝난 후에 호출되는 부분
                // 되돌아오기 애니메이션 설정
                transform.DOMove(transform.position + new Vector3(0, -2, 0), 0.1f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        // 두 번째 애니메이션이 끝나면 호출되는 부분
                        // 이곳에 추가 애니메이션이나 로직을 넣을 수 있습니다.

                        // 애니메이션이 끝났음을 플래그로 표시
                        isAnimationPlaying = false;

                        Debug.Log("Knife animation complete");
                    });
            });
    }
    IEnumerator PlayAnim(Vector3 Pos)
    {
        //DotAnim.DOPlay();
        yield return new WaitForSeconds(0.1f);
        transform.position = Pos;
        //DotAnim.DORewind();
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
