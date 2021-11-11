using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
  public static Buildings singleton;
  public BuildingMeta barrack;

  Buildings()
  {
    singleton = this;
  }

  public static BuildingMeta MetaForBuildingType(BuildingMeta.BuildingType buildingType)
  {
    switch (buildingType)
    {
      case BuildingMeta.BuildingType.Barrack:
        return singleton.barrack;
      default:
        return null;
    }
  }
}
