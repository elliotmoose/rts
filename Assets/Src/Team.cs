using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Team : MonoBehaviour
{
  public int team = 0;
  public UnityEvent onTeamSet;
  // Start is called before the first frame update
  void Start()
  {

  }

  public void SetTeam(int team)
  {
    this.team = team;
    onTeamSet.Invoke();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
