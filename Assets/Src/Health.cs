using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Team))]
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
    Selectable selectable = this.GetComponent<Selectable>();
    if (selectable.isSelected)
    {
      selectable.Deselect();
    }

    if (_healthBarRef)
    {
      GameObject.Destroy(_healthBarRef);
    }

    GameObject.Destroy(this.gameObject);
  }

  // Start is called before the first frame update
  public void Init()
  {
    _healthBarRef = GameObject.Instantiate(healthBarPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
    _healthBarRef.GetComponent<Healthbar>().Mount(this);
  }

  void Awake()
  {
    Init();
    this.GetComponent<Team>().onTeamSet.AddListener(Init);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
