using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using Cinemachine;

public class PlayerMovementLocal : NetworkBehaviour
{

    private Rigidbody rb;
    private bool onGround = true;
    private Collider collider;

    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public override void OnStartLocalPlayer() { 
        Camera.main.transform.SetParent(this.transform, false);
        var camera = FindObjectOfType<CinemachineFreeLook>();

        if (camera)
        {
            camera.LookAt = gameObject.transform;
            camera.Follow = gameObject.transform;
        }

    }



    private void FixedUpdate()
    {
        
        if(!hasAuthority)
            return;
        
        Vector3 movement = new Vector3();

        if (Keyboard.current.wKey.isPressed) { movement.z = 1 ; }
        if (Keyboard.current.sKey.isPressed) { movement.z = -1 ; }
        if (Keyboard.current.aKey.isPressed) { movement.x = -1 ; }
        if (Keyboard.current.dKey.isPressed) { movement.x = 1 ; }

        //move based on camera direction
        movement = Camera.main.transform.TransformDirection(movement);
        movement = Vector3.ProjectOnPlane(movement, Vector3.up);
        
        // to move the character and normalize to disallow from faster movement diagonally 
        movement.Normalize();
        // rb.AddForce(new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed), ForceMode.Impulse);
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);

        var lookRotation = new Vector3(rb.velocity.x, 0, rb.velocity.z);


        if (lookRotation.magnitude > 0.2f && Keyboard.current.wKey.isPressed)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        if (Keyboard.current.spaceKey.isPressed && IsGrounded())
        {
            Debug.Log("space pressed");
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0,5,0), ForceMode.Impulse);
        }
    }
    
    private bool IsGrounded()
    {
        return Physics.Raycast(collider.bounds.center, Vector3.down, collider.bounds.extents.y + 0.01f);
    }
}
