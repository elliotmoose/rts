using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Selectable : MonoBehaviour
{
  public bool isSelected
  {
    get
    {
      return Globals.SELECTED_UNITS.Contains(this);
    }
  }

  public void Select()
  {
    if (Globals.SELECTED_UNITS.Contains(this)) return;
    Globals.SELECTED_UNITS.Add(this);
    this.gameObject.GetComponent<Renderer>().material.color = Color.green;
  }

  public void Deselect()
  {
    if (!Globals.SELECTED_UNITS.Contains(this)) return;
    Globals.SELECTED_UNITS.Remove(this);
    this.gameObject.GetComponent<Renderer>().material.color = Color.white;
  }
}
