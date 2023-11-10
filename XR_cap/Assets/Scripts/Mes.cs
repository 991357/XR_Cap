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
            // A �Լ� ȣ��
            A(ray.collider.gameObject);

            //1110 ������� ����--------------------------------------------------------------------------------------------------------------------------------------------------------
        }
        else
        {
            ScanObj = null;
        }
    }

    private void A(GameObject enemy)
    {
        // �ִϸ��̼��� ���� ������ �÷��׷� ǥ��
        isAnimationPlaying = true;

        // DOTween�� ����Ͽ� Į�� Enemy���� �̵���Ű�� �ǵ��ƿ��� �ִϸ��̼� ����
        transform.DOMove(GameManager.Instance.Player.transform.position, 0.1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // �̵��� ���� �Ŀ� ȣ��Ǵ� �κ�
                // �ǵ��ƿ��� �ִϸ��̼� ����
                transform.DOMove(transform.position + new Vector3(0, -2, 0), 0.1f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        // �� ��° �ִϸ��̼��� ������ ȣ��Ǵ� �κ�
                        // �̰��� �߰� �ִϸ��̼��̳� ������ ���� �� �ֽ��ϴ�.

                        // �ִϸ��̼��� �������� �÷��׷� ǥ��
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
            //�ִϸ��̼� ����

            //�ٵ� ���� �ִϸ��̼� �������̾�?

            //�׷� return

            //�ƴϸ� ����
        }
    }
}
