using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damage = 25;
    public float range = 0.8f;


    float maxAttackCooldown = 1;
    float curAttackCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(curAttackCooldown > 0) {
            curAttackCooldown -= Time.deltaTime;   
        }
    }

    public bool DealDamage(Health toDamage) {
        bool didKill = toDamage.TakeDamage(damage);
        curAttackCooldown = maxAttackCooldown;
        Debug.Log("Damaged!");
        return didKill;
    }

    public bool InRangeToAttack(Health target) {
        return (target.transform.position - this.transform.position).magnitude <= range;
    }
    
    public bool CanAttack(Health target) {
        return curAttackCooldown <= 0 && InRangeToAttack(target);
    }
}
