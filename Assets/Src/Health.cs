using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Unit))]
public class Health : MonoBehaviour
{
  private GameObject _healthBarRef;
  public float maxHealth = 100;
  public float curHealth = 100;
  public float healthPercentage
  {
    get
    {
      return curHealth / maxHealth;
    }
  }

  public GameObject healthBarPrefab;

  Unit unit
  {
    get
    {
      return this.GetComponent<Unit>();
    }
  }

  // returns whether dead
  public bool TakeDamage(float amount)
  {
    curHealth -= amount;

    if (curHealth < 0)
    {
      Die();
      return true;
    }

    return false;
  }

  void Die()
  {
    if (Globals.SELECTED_UNITS.Contains(this.unit))
    {
      this.unit.Deselect();
    }
    if (_healthBarRef)
    {
      GameObject.Destroy(_healthBarRef);
    }
    GameObject.Destroy(this.gameObject);
  }

  // Start is called before the first frame update
  void Start()
  {
    _healthBarRef = GameObject.Instantiate(healthBarPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
    _healthBarRef.GetComponent<Healthbar>().Mount(this);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
