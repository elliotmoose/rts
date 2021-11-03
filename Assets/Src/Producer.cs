using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : Unit
{
    public GameObject unitPrefab;
    public float productionTime = 2;
    private float _curProductionTime = 0;
    public Vector3 wayPoint;

    void Start()
    {
        wayPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _curProductionTime += Time.deltaTime;
        if(_curProductionTime > productionTime) {
            _curProductionTime = 0;
            SpawnUnit();
        }
    }

    void SpawnUnit() {
        GameObject go = GameObject.Instantiate(unitPrefab, this.transform.Find("SPAWN_POSITION").position , this.transform.rotation);
        Unit unit = go.GetComponent<Unit>();
        unit.team = this.team;
        unit.CommandMove(wayPoint);
    }

    public override void CommandMove(Vector3 target) {
        this.wayPoint = target;
    }
}
