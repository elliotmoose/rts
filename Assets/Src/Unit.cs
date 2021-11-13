using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;

[RequireComponent(typeof(Selectable))]
public class Unit : NetworkBehaviour
{
  public virtual void Move(Vector3 target)
  {
    this.GetComponent<NavMeshAgent>().destination = target;
  }

  public void ClientRequestCommandMove(Vector3 target)
  {
    Debug.Log("Client move");
    CommandMoveServerRpc(target);
  }

  [ServerRpc]
  private void CommandMoveServerRpc(Vector3 target)
  {
    Debug.Log("Server move rpc");
    ServerCommandMove(target);
  }

  public virtual void ServerCommandMove(Vector3 target)
  {
    Debug.Log("Server move");
    Move(target);
  }

  public void ClientRequestCommandMoveAttack(Vector3 target)
  {
    CommandMoveAttackServerRpc(target);
  }

  [ServerRpc]
  private void CommandMoveAttackServerRpc(Vector3 target)
  {
    ServerCommandMoveAttack(target);
  }

  public virtual void ServerCommandMoveAttack(Vector3 target)
  {

  }
}
