using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Marine : Unit
{
  public enum State
  {
    MOVING,
    IDLE,
    ATTACKING
  }
  NavMeshAgent agent;
  NavMeshObstacle obstacle;
  Damager damager;

  public bool isOnAlert = false; //on alert means the unit will attacking any nearby enemy
  public float alertRange = 1.5f;
  public State state = State.IDLE;

  public Health enemyTarget;

  void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    obstacle = GetComponent<NavMeshObstacle>();
    damager = GetComponent<Damager>();
  }

  // Update is called once per frame
  void Update()
  {
    switch (this.state)
    {
      case State.ATTACKING:
        SetNavmeshIsMoving(false);

        if (enemyTarget)
        {
          if (damager.CanAttack(enemyTarget))
          {
            bool didKill = damager.DealDamage(enemyTarget.GetComponent<Health>());
            if (didKill)
            {
              enemyTarget = null;
            }
          }
        }
        else
        {
          SetState(State.IDLE);
        }

        break;

      case State.IDLE:
        SetNavmeshIsMoving(false);
        break;

      case State.MOVING:
        SetNavmeshIsMoving(true);
        // todo: scale this based on no of units selected. Also add a time buffer that checks how long the unit has been hovering the current spot
        if ((agent.destination - transform.position).magnitude < 0.1f)
        {
          SetState(State.IDLE);
        }
        break;
    }

    if (isOnAlert && !enemyTarget)
    {
      this.enemyTarget = NearestTargetInRange();
    }

    if (enemyTarget)
    {
      if (damager.InRangeToAttack(enemyTarget))
      {
        SetState(State.ATTACKING);
      }
      else
      {
        Move(enemyTarget.transform.position);
      }
    }
  }

  void SetState(State newState)
  {
    this.state = newState;
  }

  //becomes an obstacle when not moving
  void SetNavmeshIsMoving(bool moving)
  {
    if (moving)
    {
      obstacle.enabled = false;
      agent.enabled = true;
    }
    else
    {
      agent.enabled = false;
      obstacle.enabled = true;
    }
  }

  Health NearestTargetInRange()
  {
    // acquier enemy target
    Collider[] hits = Physics.OverlapSphere(this.transform.position, alertRange);
    if (hits.Length > 0)
    {
      var orderedHits = hits.OrderBy(c => (this.transform.position - c.transform.position).sqrMagnitude).Where(c => c.gameObject != this.gameObject).ToArray();

      foreach (Collider hit in orderedHits)
      {
        // todo: check if different team
        Health health = hit.gameObject.GetComponent<Health>();
        Team team = hit.gameObject.GetComponent<Team>();
        if (team.team != this.GetComponent<Team>().team && health)
        {
          return hit.gameObject.GetComponent<Health>();
        }
      }
    }

    return null;
  }

  public override void Move(Vector3 target)
  {
    this.enemyTarget = null;
    this.state = State.MOVING;
    obstacle.enabled = false;
    agent.enabled = true;
    base.Move(target);
  }

  public override void CommandMoveAttack(Vector3 target)
  {
    isOnAlert = true;
    Move(target);
  }

  public override void CommandMove(Vector3 target)
  {
    isOnAlert = false;
    Move(target);
  }
}
