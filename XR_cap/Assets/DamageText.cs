using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public float MoveSpeed;
    public float AlphaSpeed;
    Text Text;
    Color Alpha;

    private void Awake()
    {
        Text = GetComponent<Text>();
        Alpha = Text.color;
    }
    private void Update()
    {
        transform.Translate(new Vector3(0, MoveSpeed * Time.deltaTime, 0));
        Alpha.a = Mathf.Lerp(Alpha.a, 0, Time.deltaTime * AlphaSpeed);
        Text.color = Alpha;
    }
}
