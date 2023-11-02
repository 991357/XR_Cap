using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public string Name;

    public int PatternIndex;
    public int CurPatternCount;
    public int[] MaxPatternCount;

    bool IsThunder;

    Animator Anim;

    GameObject Thunder;
    GameObject Cloud;

    Enemy Enemy;
    private void Awake()
    {
        Anim = GetComponent<Animator>();
        Enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        Invoke("Think", 3f);
    }

    void Think()
    {
        PatternIndex = PatternIndex == 3 ? 0 : PatternIndex + 1;
        CurPatternCount = 0;

        if (Name == "BossA")
        {
            switch (PatternIndex)
            {
                case 0:
                    SpawnBaby();
                    break;
            }
        }
        else if(Name == "BossB")
        {
            switch (PatternIndex)
            {
                case 0:
                    ArcThunder();
                    break;
                case 1:
                    MakeCloud();
                    break;
            }
        }
        else if(Name == "BossC")
        {
            switch (PatternIndex)
            {
                case 0:
                    AcidSpawn();
                    break;
            }
        }
    }

    void SpawnBaby()
    {
        if (Enemy.F_Health < 0)
            return;

        Anim.SetTrigger("Stop");
        GameObject baby = GameManager.Instance.P_Manager.Get(16);
        baby.transform.position = new Vector2(transform.position.x + Random.Range(-3, 3), transform.position.y + Random.Range(-3, 3));
        baby.transform.rotation = transform.rotation;

        CurPatternCount++;

        if (CurPatternCount < MaxPatternCount[PatternIndex])
            Invoke("SpawnBaby", 5f);
        else
            Invoke("Think", 3f);
    }

    void ArcThunder()
    {
        if (Enemy.F_Health < 0)
            return;

        Thunder = GameManager.Instance.P_Manager.Get(17);
        Thunder.transform.SetParent(this.transform);
        Thunder.transform.position = new Vector3(transform.position.x + 0.5f,transform.position.y + 1,0);

        Invoke("ActiveFalse", 1f);

        CurPatternCount++;

        if (CurPatternCount < MaxPatternCount[PatternIndex])
            Invoke("ArcThunder", 2f);
        else
            Invoke("Think", 3f);
    }

    void MakeCloud()
    {
        if (Enemy.F_Health < 0)
            return;

        Cloud = GameManager.Instance.P_Manager.Get(18);

        if (CurPatternCount < MaxPatternCount[PatternIndex])
            Invoke("MakeCloud", 5f);
        else
            Invoke("Think", 3f);
    }


    void AcidSpawn()
    {
        if (Enemy.F_Health < 0)
            return;

        Anim.SetTrigger("Attack");

        if(gameObject.activeInHierarchy)
            StartCoroutine(AcidLogic());

        CurPatternCount++;

        if (CurPatternCount < MaxPatternCount[PatternIndex])
            Invoke("AcidSpawn", 2.5f);
        else
            Invoke("Think", 3f);
    }

    IEnumerator AcidLogic()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.P_Manager.Get(20);
    }

    void Update()
    {
    }

    void ActiveFalse()
    {
        Thunder.SetActive(false);
    }
}
