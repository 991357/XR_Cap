using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowArea : MonoBehaviour
{
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemylogic = collision.GetComponent<Enemy>();
            StartCoroutine(EnemySlow(enemylogic));
        }
    }

    IEnumerator EnemySlow(Enemy enemylogic)
    {
        switch (enemylogic.Name)
        {
            case "A":
                enemylogic.F_Speed = 1.5f;
                break;
            case "B":
                enemylogic.F_Speed = 1.7f;
                break;
            case "C":
                enemylogic.F_Speed = 2f;
                break;
            case "BossA":
                enemylogic.F_Speed = 0.7f;
                break;
            case "B_A":
                enemylogic.F_Speed = 3f;
                break;
            case "BossB":
                enemylogic.F_Speed = 1.5f;
                break;
            case "BossC":
                enemylogic.F_Speed = 1.8f;
                break;
        }
        yield return new WaitForSeconds(3);

        switch (enemylogic.Name)
        {
            case "A":
                enemylogic.F_Speed = 2f;
                break;
            case "B":
                enemylogic.F_Speed = 2.2f;
                break;
            case "C":
                enemylogic.F_Speed = 2.5f;
                break;
            case "BossA":
                enemylogic.F_Speed = 1.2f;
                break;
            case "B_A":
                enemylogic.F_Speed = 3.5f;
                break;
            case "BossB":
                enemylogic.F_Speed = 2f;
                break;
            case "BossC":
                enemylogic.F_Speed = 2.3f;
                break;
        }
    }
}
