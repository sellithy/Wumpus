using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// This class will determine when the player has clicked the mouse, and figure out what door they clicked on
/// It will also pass this information to the main game controller
/// </summary>
public class MouseController : MonoBehaviour
{
    [SerializeField]
    MainGameController mgc;

    //Are we shooting arrows
    private bool combatModeEnabled = false;

    [SerializeField]
    private GameObject[] doorOutlines = new GameObject[6];

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            //Enable/disable combat mode
            if (combatModeEnabled)
            {
                combatModeEnabled = false;
            } else
            {
                combatModeEnabled = true;
            }
            UpdateDoors();
        }

        //When is the mouse clicked
        if (Input.GetMouseButtonUp(0) && combatModeEnabled)
        {
            //We are NOT over a UI object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //go.transform.position = Camera.main.transform.position;
                //go.transform.LookAt(hit.point);
                //go.transform.localScale = new Vector3(.1f, .1f, 10000);
                //Did we hit a door?
                if (hit.transform.name.Contains("Door"))
                {
                    //What door?
                    char direction = hit.transform.name[4];

                    //Pass the value to the main game controller
                    mgc.ShootingArrow(direction);
                    combatModeEnabled = false;
                    for (int i = 0; i < doorOutlines.Length; i++)
                        doorOutlines[i].SetActive(false);
                }
            }
        }
    }

    public void UpdateCombateMode(bool newValue)
    {
        combatModeEnabled = newValue;
        UpdateDoors();
    }

    public void UpdateDoors()
    {
        for (int i = 0; i < doorOutlines.Length; i++)
        {
            doorOutlines[i].SetActive(false);
        }

        for (int i = 0; i < mgc.master.GetDoors().Length; i++)
        {
            if (combatModeEnabled)
                doorOutlines[mgc.master.GetDoors()[i]].SetActive(true);
        }
    }
}