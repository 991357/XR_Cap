using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour
{
    private void Start()
    {   
        StartCoroutine(ReMove());
    }

    IEnumerator ReMove()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
