using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject Obj_OptionPanel;
    public GameObject Obj_LanPanel;
    public GameObject Obj_VolPanel;
    public GameObject Obj_GraphicPanel;
    //public GameObject Obj_GameModePanel;
    //public GameObject Obj_GameStartBtn;
    //public GameObject Obj_ExitBtn;

    public GameObject LanguageNum;
    public GameObject Bgm;

    public void OnClickStartBtn()
    {
        //Obj_GameStartBtn.SetActive(false);
        //Obj_ExitBtn.SetActive(false);
        //Obj_GameModePanel.SetActive(true);
    }

    public void OnClickMode1Btn()//
    {
        SceneManager.LoadScene(1);
        DontDestroyOnLoad(LanguageNum);
    }
    public void OnClickOptionBtn()
    {
        Obj_OptionPanel.SetActive(true);
    }

    public void OnClickOptionExitBtn()
    {
        Obj_OptionPanel.SetActive(false);
    }

    public void OnClickMainExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickMuteBtn()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

    public void OnClickGoMain()
    {
        Obj_OptionPanel.SetActive(false);
    }

    public void OnClickLanguageBtn()
    {
        Obj_LanPanel.SetActive(true);
    }

    public void OnClickVolumeBtn()
    {
        Obj_VolPanel.SetActive(true);
    }

    public void OnClickVolExitBtn()
    {
        Obj_VolPanel.SetActive(false);
    }

    public void OnClickGraphicsBtn()
    {
        Obj_GraphicPanel.SetActive(true);
    }
}
