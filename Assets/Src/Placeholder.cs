using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    if (GetComponent<Unit>())
    {
      GetComponent<Unit>().enabled = false;
    }
    if (GetComponent<Team>())
    {
      GetComponent<Team>().enabled = false;
    }
  }

  // Update is called once per frame
  void Update()
  {
    bool canPlace = CanPlace();
    Renderer[] renderers = this.GetComponents<Renderer>();
    foreach (Renderer renderer in renderers)
    {
      renderer.material.color = canPlace ? Color.green : Color.red;
    }
  }

  public bool CanPlace()
  {
    Collider[] cols = Physics.OverlapBox(this.transform.position, Vector3.one / 2, Quaternion.identity, LayerMask.GetMask(new string[] { "Building" }));
    return cols.Length == 0;
  }
}
