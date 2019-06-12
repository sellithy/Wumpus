using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiroController : MonoBehaviour
{
    // The fields we want to be able to tweak/ set in the Unity Editor
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private Rigidbody body;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer spRend;


    // Update is called once per frame
    void Update()
    {
        // Get direction values from keyboard or joystick.
        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime; // left-right/west-east
        float z = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime; // up-down/north-south

        // character walks in x-z plane; y-component (jumping/falling) is 0
        this.body.MovePosition(transform.position + new Vector3(x, 0, z));

        //If we are moving at all, update the animator
        if (Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0)
        {
            //We are walking
            anim.SetBool("IsWalking", true);

            //Are we walking left or right
            if (x < 0)
            {
                //Walking Left
                spRend.flipX = true;
            } else
            {
                //Walking Right
                spRend.flipX = false;
            }
        } else
        {
            //We are idle
            anim.SetBool("IsWalking", false);
        }
    }
}
