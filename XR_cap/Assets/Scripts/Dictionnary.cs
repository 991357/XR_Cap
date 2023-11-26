using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionnary : MonoBehaviour
{
    public GameObject[] Cards;

    public void FireBall()
    {
        for(int i = 0; i < Cards.Length; i ++)
        {
            Cards[i].SetActive(false);
        }

        Cards[0].SetActive(true);

    }
    public void BlazeWall()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].SetActive(false);
        }

        Cards[1].SetActive(true);

    }

    public void Meteo()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].SetActive(false);
        }

        Cards[6].SetActive(true);

    }
    public void IceKnife()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].SetActive(false);
        }

        Cards[2].SetActive(true);

    }
    public void Rolling()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].SetActive(false);
        }

        Cards[3].SetActive(true);

    }
    public void AbsoluteZero()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].SetActive(false);
        }

        Cards[7].SetActive(true);

    }
    public void Mes()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].SetActive(false);
        }

        Cards[4].SetActive(true);

    }
    public void Slow()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].SetActive(false);
        }

        Cards[5].SetActive(true);
    }

    public void AllFalse()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].SetActive(false);
        }
    }
}
