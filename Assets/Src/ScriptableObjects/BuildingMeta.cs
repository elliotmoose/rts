using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingMeta", menuName = "ScriptableObjects/BuildingMeta", order = 1)]
public class BuildingMeta : ScriptableObject
{
  public string buildingName;
  public float cost;
  public float buildDuration;
  public GameObject prefab;
  public GameObject placeholderPrefab;
  public List<UnitMeta> spawnableUnits;
}