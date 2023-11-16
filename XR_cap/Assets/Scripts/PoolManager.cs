using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Prefab들을 보관할 변수가 필요
    // Pool 담당을 할 List들이 필요
    // Prefab 이 2개라면 List도 2개가 필요한법!

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
        //만약 다른 Script에서 이 Get을 쓰면 필요한 prefab이 있을텐데
        //몇번째 프리팹을 호출할것인지 i로 전달 받을텐데
        //선택한 풀에 놀고 있는 오브젝트 접근
        //만약 놀고 있는 오브젝트를 발견했다면? -> Select 변수에 할당함
        //근데 모든 오브젝트가 사용중이라면?    -> 새롭게 생성해서 Select에 할당함

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
