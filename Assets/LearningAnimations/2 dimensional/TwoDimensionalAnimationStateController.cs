using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{
    
    private Animator animator;
    private float velocityX = 0f;
    private float velocityZ = 0f;
    private float acceleration = 8.0f;
    private float deceleration = 8.0f;
    private float maxWalkVelocity = 0.5f;
    private float maxRunVelocity = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backwardPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;
        
        // -------------------- acceleration section -----------------------

        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        
        if (backwardPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        
        // -------------------- deceleration section -----------------------

        //decrease VelocityZ
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        if (!backwardPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }

        // for the backward now
        if (!forwardPressed && !backwardPressed && velocityZ != 0.0f && (velocityZ > -0.08f && velocityZ < 0.08f))
        {
            velocityZ = 0;
        }
        

        // ------------- x ----------
        
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.08f && velocityX < 0.08f))
        {
            velocityX = 0;
        }
        animator.SetFloat("Velocity X", velocityX);
        animator.SetFloat("Velocity Z", velocityZ);
        
    }
}
