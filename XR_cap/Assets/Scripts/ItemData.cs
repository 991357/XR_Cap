using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Ice_1, Ice_2, Fire_1, Shoe, Heal, Fire_2}

    [Header("# Main Info")]
    public ItemType Type;
    public int I_ItemId;
    public string S_ItemName;
    [TextArea]
    public string S_ItemDesc;
    public Sprite SP_ItemIcon;

    [Header("# Level Data")]
    public float F_BaseDmg;
    public int I_BaseCount;
    public float[] F_Dmgs;
    public int[] I_Counts;

    [Header("# Weapon")]
    public GameObject PB_Projectile;

}
