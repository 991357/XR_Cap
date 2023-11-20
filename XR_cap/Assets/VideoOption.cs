using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    public Dropdown ResolutionsDropdown;
    public Toggle FullScreenBtn;
    List<Resolution> resolutions = new List<Resolution>();
    FullScreenMode ScreenMode;
    int ResolutionNum;

    void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if(Screen.resolutions[i].refreshRate == 60)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }

        ResolutionsDropdown.options.Clear();

        int optionnum = 0;

        foreach (Resolution rs in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = rs.width + " x " + rs.height + " " + rs.refreshRate + " hz";
            ResolutionsDropdown.options.Add(option);

            if (rs.width == Screen.width && rs.height == Screen.height)
                ResolutionsDropdown.value = optionnum;
            optionnum++;
        }
        ResolutionsDropdown.RefreshShownValue();
        FullScreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropBoxOptionChange(int x)
    {
        ResolutionNum = x;
    }

    public void OnClickFullScreenBtn(bool isfull)
    {
        ScreenMode = isfull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
    public void OnClivkOkBtn()
    {
        Screen.SetResolution(resolutions[ResolutionNum].width, resolutions[ResolutionNum].height, ScreenMode);
    }
}
