using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AbilityType
{
    Shield,
    Invisibility,
    HyperJump
}

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Ability Data")]
public class AbilityData : ScriptableObject
{
    [Header("Base Settings")]
    public AbilityType abilityType;

    [Tooltip("Cooldown между активациями в секундах")]
    public float cooldown = 5f;

    [Tooltip("Сколько длится эффект")]
    public float duration = 3f;

    [Tooltip("Дистанция прыжка (только для HyperJump)")]
    public float jumpDistance = 10f;
}
