using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitMeta", menuName = "ScriptableObjects/UnitMeta", order = 1)]
public class UnitMeta : ScriptableObject
{

  public enum AttackType
  {
    RANGE,
    MELEE,
    PROJECTILE,
  }

  public string unitName;
  public float cost;
  public float trainDuration;
  public GameObject prefab;

  public AttackType attackType = AttackType.RANGE;
  public float attackDamage = 25;
  public float maxHealth = 100;
  public float movementSpeed = 3;
}