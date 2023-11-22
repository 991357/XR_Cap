using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscManager : MonoBehaviour
{
    //����ð�
    //óġ�� ��
    //óġ�� ����
    //������ ������ (����Ʈ ��)
    //����
    //hp
    //�ӵ�

    public Text Timer;
    public Text EnemyKill;
    public Text BossKill;
    public Text Quest;
    public Text Level;
    public Text HP;

    int Hp;

    private void Update()
    {
        float remaintime = GameManager.Instance.MaxGameTime - GameManager.Instance.GameTime;
        int min = Mathf.FloorToInt(remaintime / 60);
        int sec = Mathf.FloorToInt(remaintime % 60);
        Timer.text = string.Format("{0:D2}:{1:D2}", min, sec);

        EnemyKill.text = GameManager.Instance.Kill.ToString();

        BossKill.text = GameManager.Instance.BossKillCount.ToString();

        Quest.text = GameManager.Instance.Q_Manager.QuestCount.ToString();

        Level.text = GameManager.Instance.Level.ToString();

        Hp = (int)GameManager.Instance.Health;
        HP.text = Hp.ToString();
    }
}
