using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Unit))]
public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float curHealth = 100;
    public float healthPercentage {
        get {
            return curHealth/maxHealth;
        }
    }

    Unit unit {
        get {
            return this.GetComponent<Unit>();
        }
    }

    // returns whether dead
    public bool TakeDamage(float amount) {
        curHealth -= amount;

        if(curHealth < 0) {
            Die();
            return true;
        }

        return false;
    }

    void Die() {
        if(Globals.SELECTED_UNITS.Contains(this.unit)) {
            this.unit.Deselect();
        }
        GameObject.Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
