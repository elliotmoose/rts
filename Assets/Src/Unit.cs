using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public int team = 0;
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

    public virtual void Move(Vector3 target) {
        this.GetComponent<NavMeshAgent>().destination = target;
    }

    public virtual void CommandMove(Vector3 target) {
        Move(target);
    }

    public virtual void CommandMoveAttack(Vector3 target) {

    }
}
