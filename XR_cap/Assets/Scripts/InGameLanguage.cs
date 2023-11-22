using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLanguage : MonoBehaviour
{
    public GameObject Lan_Panel;
    public void OnClickKRBtn()
    {
        GameManager.Instance.C_Manager.LNum = 0;
        Lan_Panel.SetActive(false);
    }
    public void OnClickEnBtn()
    {
        GameManager.Instance.C_Manager.LNum = 1;
        Lan_Panel.SetActive(false);
    }
    public void OnClickSPBtn()
    {
        GameManager.Instance.C_Manager.LNum = 4;
        Lan_Panel.SetActive(false);
    }
    public void OnClickJPBtn()
    {
        GameManager.Instance.C_Manager.LNum = 2;
        Lan_Panel.SetActive(false);
    }
    public void OnClickCHBtn()
    {
        GameManager.Instance.C_Manager.LNum = 3;
        Lan_Panel.SetActive(false);
    }
}
