using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement movement;

    public PlayerGun gun;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        gun.Shot(Input.GetKey("mouse 0"), Input.GetKey("mouse 1"));

    }

    void FixedUpdate()
    {
        movement.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

        movement.Aim(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}