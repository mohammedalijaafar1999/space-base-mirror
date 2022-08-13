using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    float velocity = 0f;
    float acceleration = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool("IsRunning");
        bool isWalking = animator.GetBool("IsWalking");
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        if (forwardPressed && velocity < 1f)
        {
            velocity += Time.deltaTime * acceleration;
        }
        
        if (!forwardPressed && velocity > 0)
        {
            velocity -= Time.deltaTime * acceleration;
        }

        if (velocity < 0 && !forwardPressed)
        {
            velocity = 0;
        }
        
        animator.SetFloat("Velocity", velocity);
        // if (!isWalking && forwardPressed)
        // {
        //     animator.SetBool("IsWalking", true);
        // }
        //
        // if (isWalking && !forwardPressed)
        // {
        //     animator.SetBool("IsWalking", false);
        // }
        //
        // if (!isRunning && (forwardPressed && runPressed))
        // {
        //     animator.SetBool("IsRunning", true);
        // }
        //
        // if (isRunning && (!forwardPressed || !runPressed))
        // {
        //     animator.SetBool("IsRunning", false);
        // }
    }
}
