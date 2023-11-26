using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject Char1Panel;
    public GameObject Char2Panel;
    public GameObject Char3Panel;

    public void Char1True()
    {
        Char1Panel.SetActive(true);
        Char2Panel.SetActive(false);
        Char3Panel.SetActive(false);
    }
    public void Char2True()
    {
        Char1Panel.SetActive(false);
        Char2Panel.SetActive(true);
        Char3Panel.SetActive(false);
    }
    public void Char3True()
    {
        Char1Panel.SetActive(false);
        Char2Panel.SetActive(false);
        Char3Panel.SetActive(true);
    }
    public void GoMain()
    {
        SceneManager.LoadScene(0);
    }
}
