using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health}
    public InfoType Type;

    Text T_MyText;
    Slider S_MySlider;

    private void Awake()
    {
        T_MyText = GetComponent<Text>();
        S_MySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch(Type)
        {
            case InfoType.Exp:
                float curexp = GameManager.Instance.Exp;
                float maxexp = GameManager.Instance.I_NextExp[Mathf.Min(GameManager.Instance.Level, GameManager.Instance.I_NextExp.Length - 1)];

                S_MySlider.value = curexp / maxexp;
                break;

            case InfoType.Level:
                T_MyText.text = string.Format("Lv.{0:F0}",GameManager.Instance.Level);            //0은 데이터의 index,  숫자의 대한 형태는 :찍고 다음에 오는 F0
                break;

            case InfoType.Kill:
                T_MyText.text = string.Format("{0:F0}", GameManager.Instance.Kill);
                break;
                 
            case InfoType.Time:
                float remaintime = GameManager.Instance.MaxGameTime - GameManager.Instance.GameTime;
                int min = Mathf.FloorToInt(remaintime / 60);
                int sec = Mathf.FloorToInt(remaintime % 60);
                T_MyText.text = string.Format("{0:D2}:{1:D2}", min, sec);

                break;
            case InfoType.Health:
                float curhealth = GameManager.Instance.Health;
                float maxhealth = GameManager.Instance.MaxHealth;

                S_MySlider.value = curhealth / maxhealth;
                break;
        }
    }
}
