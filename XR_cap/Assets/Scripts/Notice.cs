using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notice : MonoBehaviour
{
    public string Type;
    Animator A_anim;

    public bool IsAnim = false;
    private void Awake()
    {
        A_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(IsAnim)
        {
            if(Type == "Left")
                A_anim.SetTrigger("Left");
            IsAnim = false;
        }
    }
    private void OnEnable()
    {
        if (Type == "Right")
            A_anim.SetTrigger("On");
        else if(Type == "Left")
            A_anim.SetTrigger("Left");
    }
}
