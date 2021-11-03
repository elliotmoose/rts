using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
  public Health target;
  public int boxCount = 5;
  float verticalOffset = 50;
  List<Image> boxes = new List<Image>();
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (!target) return;
    this.transform.position = Camera.main.WorldToScreenPoint(target.transform.position) + Vector3.up * verticalOffset;

    float binnedHealth = (target.curHealth / target.maxHealth) * (float)boxCount;
    for (int i = 0; i < boxCount; i++)
    {
      if (i > boxes.Count)
      {
        return;
      }
      float alpha = Mathf.Clamp01((binnedHealth - (float)i));
      Color color = boxes[i].color;
      color.a = alpha;
      boxes[i].color = color;
    }
  }

  public void Mount(Health target)
  {
    this.target = target;

    for (int i = 0; i < boxCount; i++)
    {
      GameObject template = this.transform.GetChild(0).gameObject;
      GameObject box = GameObject.Instantiate(template, this.transform);
      boxes.Add(box.GetComponent<Image>());
    }
  }
}
