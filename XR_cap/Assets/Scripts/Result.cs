using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] Obj_Titles;

    public void Lose()
    {
        Obj_Titles[0].SetActive(true);
    }
    public void Win()
    {
        Obj_Titles[1].SetActive(true);
    }
}
