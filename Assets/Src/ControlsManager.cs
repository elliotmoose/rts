using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
  int playerTeam = 0;
  public enum Command
  {
    NONE,
    MOVE,
    ATTACK,
    BUILD
  }
  float mDelta = 10; // Pixels. The width border at the edge in which the movement work
  float cameraSpeed = 7.0f;

  Command awaitingCommand = Command.NONE;
  BuildingMeta.BuildingType awaitingBuildingType = BuildingMeta.BuildingType.NULL;
  GameObject placeholderBuilding = null;

  void Start()
  {
    Cursor.lockState = CursorLockMode.Confined;
  }

  // Update is called once per frame
  void Update()
  {
    // if ( Input.mousePosition.x >= Screen.width - mDelta )
    // {
    //     // Move the camera
    //     transform.position += Vector3.right * Time.deltaTime * cameraSpeed;
    // }


    // if ( Input.mousePosition.x <= 0 + mDelta )
    // {
    //     // Move the camera
    //     transform.position -= Vector3.right * Time.deltaTime * cameraSpeed;
    // }

    // if ( Input.mousePosition.y >= Screen.height - mDelta )
    // {

    //     // Move the camera
    //     transform.position += Vector3.forward * Time.deltaTime * cameraSpeed;
    // }

    // if ( Input.mousePosition.y <= 0 + mDelta )
    // {
    //     // Move the camera
    //     transform.position -= Vector3.forward * Time.deltaTime * cameraSpeed;
    // }

    // selection
    if (Input.GetMouseButtonDown(0))
    {
      if (this.awaitingCommand != Command.NONE)
      {
        switch (this.awaitingCommand)
        {
          case Command.ATTACK:
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
              //get target to attack 
              // allocate the same offset as the 
              foreach (Selectable selectable in Globals.SELECTED_UNITS)
              {
                Unit unit = selectable.GetComponent<Unit>();
                if (unit)
                {
                  unit.CommandMoveAttack(hit.point);
                }
              }
            }

            this.awaitingCommand = Command.NONE;
            break;

          case Command.BUILD:
            RaycastHit[] hits = RaycastMouse(new string[] { "Floor" });
            if (hits.Length > 0 && placeholderBuilding)
            {
              // build building              
              Placeholder placeholder = placeholderBuilding.GetComponent<Placeholder>();
              if (placeholder.CanPlace())
              {
                RequestBuildBuilding(hits[0].point, this.awaitingBuildingType);
                this.awaitingCommand = Command.NONE;
              }
            }
            break;
          default:
            break;
        }
      }
      else
      {
        _isDraggingMouseBox = true;
        _dragStartPosition = Input.mousePosition;
      }
    }

    if (Input.GetKey(KeyCode.Escape))
    {
      if (this.awaitingCommand != Command.NONE)
      {
        this.awaitingCommand = Command.NONE;
      }
    }

    if (Input.GetMouseButtonUp(0) && _isDraggingMouseBox)
    {
      _isDraggingMouseBox = false;
      _SelectUnitsInDraggingBox();
    }


    if (Input.GetMouseButtonUp(1))
    {
      RaycastHit hit;

      if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
      {

        Vector3 targetWaypoint = hit.point;

        // allocate the same offset as the 
        foreach (Selectable selectable in Globals.SELECTED_UNITS)
        {
          Unit unit = selectable.GetComponent<Unit>();
          if (unit)
          {
            Vector3 randomOffset = (Vector3.one - Vector3.up) * Random.Range(0, 1);
            unit.CommandMove(targetWaypoint + randomOffset);
          }
        }
      }
    }

    if (Input.GetKeyUp(KeyCode.A))
    {
      this.awaitingCommand = Command.ATTACK;
    }

    if (Input.GetKey(KeyCode.Escape))
    {
      this.awaitingCommand = Command.NONE;
    }

    if (Input.GetKey(KeyCode.B))
    {
      this.awaitingCommand = Command.BUILD;
      this.awaitingBuildingType = BuildingMeta.BuildingType.Barrack;
    }

    if (this.awaitingCommand == Command.BUILD && this.awaitingBuildingType != BuildingMeta.BuildingType.NULL)
    {
      if (!placeholderBuilding)
      {
        BuildingMeta meta = Buildings.MetaForBuildingType(this.awaitingBuildingType);
        placeholderBuilding = GameObject.Instantiate(meta.placeholderPrefab);
      }

      RaycastHit[] hits = RaycastMouse(new[] { "Floor" });
      bool hasHit = hits.Length != 0;
      placeholderBuilding.SetActive(hasHit);

      if (hasHit)
      {
        placeholderBuilding.transform.position = hits[0].point;
      }
    }
    else if (placeholderBuilding)
    {
      GameObject.Destroy(placeholderBuilding);
    }
  }

  private bool _isDraggingMouseBox = false;
  private Vector3 _dragStartPosition;

  void OnGUI()
  {
    if (_isDraggingMouseBox)
    {
      // Create a rect from both mouse positions
      var rect = Utils.GetScreenRect(_dragStartPosition, Input.mousePosition);
      Utils.DrawScreenRect(rect, new Color(0.5f, 1f, 0.4f, 0.2f));
      Utils.DrawScreenRectBorder(rect, 1, new Color(0.5f, 1f, 0.4f));
    }
  }

  private void _SelectUnitsInDraggingBox()
  {
    Bounds selectionBounds = Utils.GetViewportBounds(
        Camera.main,
        _dragStartPosition,
        Input.mousePosition
    );
    GameObject[] selectableUnits = GameObject.FindGameObjectsWithTag("Unit");
    bool inBounds;

    List<GameObject> unitsToSelect = new List<GameObject>();
    List<GameObject> unitsToDeselect = new List<GameObject>();
    foreach (GameObject unitGameObject in selectableUnits)
    {
      Unit unit = unitGameObject.GetComponent<Unit>();
      Team team = unitGameObject.GetComponent<Team>();
      if (!unit)
      {
        continue;
      }

      if (team.team != Globals.localPlayerTeam)
      {
        continue;
      }

      inBounds = selectionBounds.Contains(
          Camera.main.WorldToViewportPoint(unit.transform.position)
      );

      if (inBounds)
      {
        unitsToSelect.Add(unitGameObject);
      }
      else
      {
        unitsToDeselect.Add(unitGameObject);
      }
    }

    if (unitsToSelect.Count == 0)
    {
      // if i didnt select anything else, don't do anything
    }
    else
    {
      foreach (GameObject unit in unitsToSelect)
      {
        unit.GetComponent<Selectable>().Select();
      }
      foreach (GameObject unit in unitsToDeselect)
      {
        unit.GetComponent<Selectable>().Deselect();
      }
    }
  }

  private RaycastHit[] RaycastMouse(string[] layers, float distance = 200)
  {
    return Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), distance, LayerMask.GetMask(layers));
  }

  void RequestBuildBuilding(Vector3 position, BuildingMeta.BuildingType buildingType)
  {
    BuildingMeta meta = Buildings.MetaForBuildingType(buildingType);
    if (meta)
    {
      GameObject newBuilding = GameObject.Instantiate(meta.prefab, position, Quaternion.identity);
      Producer producer = newBuilding.GetComponent<Producer>();
      Team team = newBuilding.GetComponent<Team>();
      team.SetTeam(Globals.localPlayerTeam);
    }
  }
}
