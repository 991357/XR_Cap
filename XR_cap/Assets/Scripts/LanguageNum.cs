using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageNum : MonoBehaviour
{
    public int Lnum;
    public GameObject Obj_LanPanel;
    public void OnClickKR()
    {
        //한국어 버튼을 눌렀으면? Lnum = 0 ;
        //영어 버튼을 눌렀으면? Lnum = 1 ;
        //일본어 버튼을 눌렀으면? Lnum = 2 ;
        //중국어 버튼을 눌렀으면? Lnum = 3 ;
        //아무거나 버튼을 눌렀으면? Lnum = 4 ;

        Lnum = 0;
        Obj_LanPanel.SetActive(false);
    }
    public void OnClickEN()
    {
        //한국어 버튼을 눌렀으면? Lnum = 0 ;
        //영어 버튼을 눌렀으면? Lnum = 1 ;
        //일본어 버튼을 눌렀으면? Lnum = 2 ;
        //중국어 버튼을 눌렀으면? Lnum = 3 ;
        //아무거나 버튼을 눌렀으면? Lnum = 4 ;

        Lnum = 1;
        Obj_LanPanel.SetActive(false);
    }

    public void OnClickJP()
    {
        //한국어 버튼을 눌렀으면? Lnum = 0 ;
        //영어 버튼을 눌렀으면? Lnum = 1 ;
        //일본어 버튼을 눌렀으면? Lnum = 2 ;
        //중국어 버튼을 눌렀으면? Lnum = 3 ;
        //아무거나 버튼을 눌렀으면? Lnum = 4 ;

        Lnum = 2;
        Obj_LanPanel.SetActive(false);
    }

    public void OnClickCH()
    {
        //한국어 버튼을 눌렀으면? Lnum = 0 ;
        //영어 버튼을 눌렀으면? Lnum = 1 ;
        //일본어 버튼을 눌렀으면? Lnum = 2 ;
        //중국어 버튼을 눌렀으면? Lnum = 3 ;
        //아무거나 버튼을 눌렀으면? Lnum = 4 ;

        Lnum = 3;
        Obj_LanPanel.SetActive(false);
    }

    public void OnClickSP()
    {
        //한국어 버튼을 눌렀으면? Lnum = 0 ;
        //영어 버튼을 눌렀으면? Lnum = 1 ;
        //일본어 버튼을 눌렀으면? Lnum = 2 ;
        //중국어 버튼을 눌렀으면? Lnum = 3 ;
        //아무거나 버튼을 눌렀으면? Lnum = 4 ;
        Lnum = 4;
        Obj_LanPanel.SetActive(false);
    }
}
