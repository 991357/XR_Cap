using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mes : MonoBehaviour
{
    public GameObject HitPoint;
    GameObject ScanObj;
    bool IsAttack;
    // Start is called before the first frame update
    void Start()
    {
        
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
            StartCoroutine(Attack());
        }
        else
        {
            ScanObj = null;
        }
    }

    IEnumerator Attack()
    {        
        IsAttack = true;

        //IsAttack �߿��� ���̻� �ٸ� ScanObj�� ã�� ���ϵ��� ����

        //transform.position�� scanobj������ �̵���Ų��
        //Ȥ�� ������ �̵��ϰ� �ǵ��� �´�

        transform.position = ScanObj.transform.position;

        yield return new WaitForSeconds(0.1f);

        //1109 ������� ����

        //Enemy���� �� �¾Ҵٰ� ��ȣ���ְ�

        IsAttack = false;
    }
}
