using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Selectable))]
public class Unit : MonoBehaviour
{
  public virtual void Move(Vector3 target)
  {
    this.GetComponent<NavMeshAgent>().destination = target;
  }

  public virtual void CommandMove(Vector3 target)
  {
    Move(target);
  }

  public virtual void CommandMoveAttack(Vector3 target)
  {

  }
}
