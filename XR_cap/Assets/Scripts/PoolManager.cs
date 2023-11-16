using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Prefab���� ������ ������ �ʿ�
    // Pool ����� �� List���� �ʿ�
    // Prefab �� 2����� List�� 2���� �ʿ��ѹ�!

    public GameObject[] Obj_Prefabs;

    public List<GameObject>[] L_Pools;

    private void Awake()
    {
        L_Pools = new List<GameObject>[Obj_Prefabs.Length];

        for (int i = 0; i < L_Pools.Length; i++)
            L_Pools[i] = new List<GameObject>();
    }

    public GameObject Get(int i)
    {
        GameObject Select = null;
        //���� �ٸ� Script���� �� Get�� ���� �ʿ��� prefab�� �����ٵ�
        //���° �������� ȣ���Ұ����� i�� ���� �����ٵ�
        //������ Ǯ�� ��� �ִ� ������Ʈ ����
        //���� ��� �ִ� ������Ʈ�� �߰��ߴٸ�? -> Select ������ �Ҵ���
        //�ٵ� ��� ������Ʈ�� ������̶��?    -> ���Ӱ� �����ؼ� Select�� �Ҵ���

        foreach(GameObject item in L_Pools[i])
        {
            if (!item.activeSelf)
            {
                Select = item;
                Select.SetActive(true);
                break;
            }
        }

        if(Select == null)
        {
            Select = Instantiate(Obj_Prefabs[i], transform);
            L_Pools[i].Add(Select);
        }
        return Select;
    }
}
