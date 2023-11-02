using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D C_Coll;
    private void Awake()
    {
        C_Coll = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 V_PlayerPos = GameManager.Instance.Player.transform.position;
        Vector3 V_MyPos = transform.position;

        float F_Dirx = V_PlayerPos.x - V_MyPos.x;
        float F_Diry = V_PlayerPos.y - V_MyPos.y;

        float F_Diffx = Mathf.Abs(F_Dirx);   //x축과 y축 각각의 거리 
        float F_Diffy = Mathf.Abs(F_Diry);

        F_Dirx = F_Dirx > 0 ? 1 : -1;
        F_Diry = F_Diry > 0 ? 1 : -1;

        Vector3 V_PlayerDir = GameManager.Instance.PlayerLogic.InputVec;

        switch (transform.tag)
        {
            case "Ground":
                if(F_Diffx > F_Diffy)
                {
                    transform.Translate(Vector3.right * F_Dirx * 80);
                }
                else if(F_Diffx < F_Diffy)
                {
                    transform.Translate(Vector3.up * F_Diry * 80);
                }
                break;
            case "Enemy":
                if (C_Coll.enabled)
                {
                    Vector3 dist = V_PlayerPos - V_MyPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3),0);
                    transform.Translate(ran + dist * 2);
                }
                break;
        }
    }
}
