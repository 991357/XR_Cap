using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageNum : MonoBehaviour
{
    public int Lnum;
    public GameObject Obj_LanPanel;
    public void OnClickKR()
    {
        //�ѱ��� ��ư�� ��������? Lnum = 0 ;
        //���� ��ư�� ��������? Lnum = 1 ;
        //�Ϻ��� ��ư�� ��������? Lnum = 2 ;
        //�߱��� ��ư�� ��������? Lnum = 3 ;
        //�ƹ��ų� ��ư�� ��������? Lnum = 4 ;

        Lnum = 0;
        Obj_LanPanel.SetActive(false);
    }
    public void OnClickEN()
    {
        //�ѱ��� ��ư�� ��������? Lnum = 0 ;
        //���� ��ư�� ��������? Lnum = 1 ;
        //�Ϻ��� ��ư�� ��������? Lnum = 2 ;
        //�߱��� ��ư�� ��������? Lnum = 3 ;
        //�ƹ��ų� ��ư�� ��������? Lnum = 4 ;

        Lnum = 1;
        Obj_LanPanel.SetActive(false);
    }

    public void OnClickJP()
    {
        //�ѱ��� ��ư�� ��������? Lnum = 0 ;
        //���� ��ư�� ��������? Lnum = 1 ;
        //�Ϻ��� ��ư�� ��������? Lnum = 2 ;
        //�߱��� ��ư�� ��������? Lnum = 3 ;
        //�ƹ��ų� ��ư�� ��������? Lnum = 4 ;

        Lnum = 2;
        Obj_LanPanel.SetActive(false);
    }

    public void OnClickCH()
    {
        //�ѱ��� ��ư�� ��������? Lnum = 0 ;
        //���� ��ư�� ��������? Lnum = 1 ;
        //�Ϻ��� ��ư�� ��������? Lnum = 2 ;
        //�߱��� ��ư�� ��������? Lnum = 3 ;
        //�ƹ��ų� ��ư�� ��������? Lnum = 4 ;

        Lnum = 3;
        Obj_LanPanel.SetActive(false);
    }

    public void OnClickSP()
    {
        //�ѱ��� ��ư�� ��������? Lnum = 0 ;
        //���� ��ư�� ��������? Lnum = 1 ;
        //�Ϻ��� ��ư�� ��������? Lnum = 2 ;
        //�߱��� ��ư�� ��������? Lnum = 3 ;
        //�ƹ��ų� ��ư�� ��������? Lnum = 4 ;
        Lnum = 4;
        Obj_LanPanel.SetActive(false);
    }
}
