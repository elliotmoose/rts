using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Selectable))]
public class Producer : Unit
{
  public GameObject unitPrefab;
  public float productionTime = 2;
  private float _curProductionTime = 0;

  Transform waypoint
  {
    get
    {
      return this.transform.Find("WAYPOINT");
    }
  }

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    UpdateSpawnUnit();
    RenderWaypoint();
  }

  void UpdateSpawnUnit()
  {
    _curProductionTime += Time.deltaTime;
    if (_curProductionTime > productionTime)
    {
      _curProductionTime = 0;
      SpawnUnit();
    }
  }

  void RenderWaypoint()
  {
    LineRenderer lineRenderer = this.GetComponent<LineRenderer>();
    bool isSelected = this.GetComponent<Selectable>().isSelected;
    lineRenderer.enabled = isSelected;
    if (isSelected)
    {
      lineRenderer.SetPosition(0, this.transform.position);
      lineRenderer.SetPosition(1, waypoint.position);
    }
  }
  void SpawnUnit()
  {
    if (!IsServer) return;
    GameObject go = GameObject.Instantiate(unitPrefab, this.transform.Find("SPAWN_POSITION").position, this.transform.rotation);
    go.GetComponent<Team>().SetTeam(this.GetComponent<Team>().team);
    go.GetComponent<Unit>().ServerCommandMove(waypoint.position);
    go.GetComponent<NetworkObject>().Spawn();
  }

  public override void ServerCommandMove(Vector3 target)
  {
    waypoint.position = target;
  }
}
