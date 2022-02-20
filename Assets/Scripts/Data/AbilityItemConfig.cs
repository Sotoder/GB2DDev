﻿using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityItem", menuName = "AbilityItem", order = 0)]
public class AbilityItemConfig : ScriptableObject
{
    public ItemConfig Item;
    public GameObject View;
    public AbilityType Type;
    public float value;
    public float duration;
    public float cooldown;
    public int Id => Item.Id;
}