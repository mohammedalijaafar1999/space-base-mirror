using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    
    [Header("Settings")] 
    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody rb;

    private void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }


    [Client]
    void Update()
    {
        if (!hasAuthority)
            return;

        var movement = new Vector3();

        if (Keyboard.current.wKey.isPressed) { movement.z += 1; }
        if (Keyboard.current.sKey.isPressed) { movement.z -= 1; }
        if (Keyboard.current.aKey.isPressed) { movement.x -= 1; }
        if (Keyboard.current.dKey.isPressed) { movement.x += 1; }
        
        //move based on camera direction
        movement = Camera.main.transform.TransformDirection(movement);
        movement = Vector3.ProjectOnPlane(movement, Vector3.up);
        
        // to move the character and normalize to disallow from faster movement diagonally 
        // rb.velocity = movement.normalized * (movementSpeed * Time.deltaTime);
        //
        // if (rb.velocity.magnitude > 0.2f)
        // {
        //     transform.rotation = Quaternion.LookRotation(movement);    
        // }
        
        transform.Translate(movement.normalized * (movementSpeed * Time.deltaTime));

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);    
        }
        
        


    }
}
