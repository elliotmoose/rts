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
}
