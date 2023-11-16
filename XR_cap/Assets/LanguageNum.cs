using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageNum : MonoBehaviour
{
    public int Lnum;

    public void OnClickKR()
    {
        //한국어 버튼을 눌렀으면? Lnum = 0 ;
        //영어 버튼을 눌렀으면? Lnum = 1 ;
        //일본어 버튼을 눌렀으면? Lnum = 2 ;
        //중국어 버튼을 눌렀으면? Lnum = 3 ;
        //아무거나 버튼을 눌렀으면? Lnum = 4 ;

        Lnum = 0;
    }
    public void OnClickEN()
    {
        //한국어 버튼을 눌렀으면? Lnum = 0 ;
        //영어 버튼을 눌렀으면? Lnum = 1 ;
        //일본어 버튼을 눌렀으면? Lnum = 2 ;
        //중국어 버튼을 눌렀으면? Lnum = 3 ;
        //아무거나 버튼을 눌렀으면? Lnum = 4 ;

        Lnum = 1;
    }

    public void OnClickJP()
    {
        //한국어 버튼을 눌렀으면? Lnum = 0 ;
        //영어 버튼을 눌렀으면? Lnum = 1 ;
        //일본어 버튼을 눌렀으면? Lnum = 2 ;
        //중국어 버튼을 눌렀으면? Lnum = 3 ;
        //아무거나 버튼을 눌렀으면? Lnum = 4 ;

        Lnum = 2;
    }

    public void OnClickCH()
    {
        //한국어 버튼을 눌렀으면? Lnum = 0 ;
        //영어 버튼을 눌렀으면? Lnum = 1 ;
        //일본어 버튼을 눌렀으면? Lnum = 2 ;
        //중국어 버튼을 눌렀으면? Lnum = 3 ;
        //아무거나 버튼을 눌렀으면? Lnum = 4 ;

        Lnum = 3;
    }

    public void OnClickSP()
    {
        //한국어 버튼을 눌렀으면? Lnum = 0 ;
        //영어 버튼을 눌렀으면? Lnum = 1 ;
        //일본어 버튼을 눌렀으면? Lnum = 2 ;
        //중국어 버튼을 눌렀으면? Lnum = 3 ;
        //아무거나 버튼을 눌렀으면? Lnum = 4 ;
        Lnum = 4;
    }
}
