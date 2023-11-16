using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeArea : MonoBehaviour
{
    private void OnEnable()
    {
        switch(GameManager.Instance.Player.Power)
        {
            case 1:
                StartCoroutine(Level1());
                break;
            case 2:
                Level2();
                break;
            case 3:
                //StartCoroutine(Level3());
                break;
        }
    }
    
    IEnumerator Level1()
    {
        //½½·Î¿ì

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().F_Speed -= 1;
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().F_Speed += 1;
        }
    }

    void Level2()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().IsFreeze = true;
        }
    }

    //IEnumerator Level3()
    //{
    //    // ?
    //}
}
