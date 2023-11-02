using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    GameObject Target;

    private void Update()
    {
        transform.position = Target.transform.position;
    }
}
