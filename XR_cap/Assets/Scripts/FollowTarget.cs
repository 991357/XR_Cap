using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject Target;
    private void Update()
    {
        transform.position = new Vector3(Target.transform.position.x + 0.64f, Target.transform.position.y + 2.5f, Target.transform.position.z);
    }
}
