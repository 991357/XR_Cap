using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject Obj_OptionPanel;

    public void OnClickStartBtn()
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
