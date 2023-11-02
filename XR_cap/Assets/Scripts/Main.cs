using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject Obj_OptionPanel;
    public GameObject Obj_GameModePanel;
    public GameObject Obj_GameStartBtn;
    public GameObject Obj_ExitBtn;

    public void OnClickStartBtn()
    {
        Obj_GameStartBtn.SetActive(false);
        Obj_ExitBtn.SetActive(false);
        Obj_GameModePanel.SetActive(true);
    }

    public void OnClickMode1Btn()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickOptionBtn()
    {
        gameObject.SetActive(false);
        Obj_OptionPanel.SetActive(true);
    }

    public void OnClickOptionExitBtn()
    {
        gameObject.SetActive(true);
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
}
