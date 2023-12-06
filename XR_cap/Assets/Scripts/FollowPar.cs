using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPar : MonoBehaviour
{
    public GameObject Enemy;

    private void Update()
    {
        transform.position = new Vector2(Enemy.transform.position.x + 0.5f, Enemy.transform.position.y - 1.5f);
    }
}
