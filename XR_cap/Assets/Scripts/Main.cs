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
    public GameObject Obj_DictionaryPanel;
    public GameObject Obj_GuidePanel;
    public SfxManager SfxManager;
    public GameObject Par;
    //public GameObject Obj_GameModePanel;
    //public GameObject Obj_GameStartBtn;
    //public GameObject Obj_ExitBtn;

    public GameObject LanguageNum;
    public GameObject Bgm;
    public Dictionnary DIc_Logic;

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void OnClickStartBtn()
    {
        //Obj_GameStartBtn.SetActive(false);
        //Obj_ExitBtn.SetActive(false);
        //Obj_GameModePanel.SetActive(true);
    }

    public void OnClickMode1Btn()//
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        SceneManager.LoadScene(1);
        DontDestroyOnLoad(LanguageNum);
    }
    public void OnClickOptionBtn()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Par.SetActive(false);
        Obj_OptionPanel.SetActive(true);
    }

    public void OnClickOptionExitBtn()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
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
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Par.SetActive(true);
        Obj_OptionPanel.SetActive(false);
    }

    public void OnClickLanguageBtn()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Obj_LanPanel.SetActive(true);
    }

    public void OnClickVolumeBtn()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Obj_VolPanel.SetActive(true);
    }

    public void OnClickVolExitBtn()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Obj_VolPanel.SetActive(false);
    }

    public void OnClickGraphicsBtn()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Obj_GraphicPanel.SetActive(true);
    }

    public void OnClickGraExitBtn()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Obj_GraphicPanel.SetActive(false);
    }

    public void OnClickGameExitBtn()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        SceneManager.LoadScene(0);
    }

    public void OnClickDictionnaryBtn()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Obj_DictionaryPanel.SetActive(true);
    }
    public void OnClickDictionnaryExitBtn()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        DIc_Logic.AllFalse();
        Obj_DictionaryPanel.SetActive(false);
    }
    
    public void OnClickGuide()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Obj_GuidePanel.SetActive(true);
    }
    public void OnClickGuideExit()
    {
        SfxManager.Instance.PlaySfx(SfxManager.Sfx.Click);
        Obj_GuidePanel.SetActive(false);
    }
}
