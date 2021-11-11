using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingMeta", menuName = "ScriptableObjects/BuildingMeta", order = 1)]
public class BuildingMeta : ScriptableObject
{
  public enum BuildingType
  {
    NULL = -1,
    Barrack = 0
  }

  public BuildingType buildingType;
  public string buildingName;
  public float cost;
  public float buildDuration;
  public GameObject prefab;
  public GameObject placeholderPrefab;
  public List<UnitMeta> spawnableUnits;
}