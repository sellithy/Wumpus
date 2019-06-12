using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WumpusEngine;

public class HexController : MonoBehaviour
{
    [SerializeField]
    private Material material;
    private GameControl master;

    // Start is called before the first frame update
    void Start()
    {
        master = GameControl.GetMaintained();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (this.master.GetState())
        {
            case States.Wandering:
                // start the minigame!
                this.master.GetDoors();
                this.master.Direction(this.name[4]);
                this.master.IsHazardsInRoom();
                break;
            default:
                // nothing more to do -- get out of here
                return;
        }
        collision.transform.position = new Vector3(0, 0.1f, 0);
        
    }
}
