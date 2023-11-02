using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float F_ScanRange;
    public LayerMask LM_TargetLayer;
    public RaycastHit2D[] RH_Targets;
    public Transform F_NearstTarget;

    private void FixedUpdate()
    {
        RH_Targets = Physics2D.CircleCastAll(transform.position, F_ScanRange, Vector2.zero, 0, LM_TargetLayer);            //������ ĳ��Ʈ�� ��� ��� ����� ��ȯ�ϴ� �Լ�\
        F_NearstTarget = GetNearst();
    }

    Transform GetNearst()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in RH_Targets)
        {
            Vector3 mypos = transform.position;
            Vector3 tarpos = target.transform.position;

            float curdiff = Vector3.Distance(mypos, tarpos);

            if(curdiff < diff)
            {
                diff = curdiff;
                result = target.transform;
            }
        }

        return result;
    }
}
