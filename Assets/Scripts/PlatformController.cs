using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
	public float torque;
    Rigidbody rb;
    public GameManager gameMgrScript;
    
    public bool isGame = false;
    
    private int touchAxis = 0;

    bool isTouchingBall = false;

    [SerializeField] ParticleSystem smashParticles;
    [SerializeField] Rigidbody ballRb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
    	if (isGame)
    	{
    		float turn = Input.GetAxis("Horizontal") + touchAxis;
        	rb.AddTorque(-transform.forward * torque * turn);

            //var emission = smashParticles.emission;
             //   emission.rateOverTime = ballRb.velocity.magnitude * 2;

            Debug.Log(ballRb.velocity.magnitude);
            Debug.Log(isTouchingBall);

            if (isTouchingBall && ballRb.velocity.magnitude > 1)
            {
                var emission = smashParticles.emission;
                emission.rateOverTime = ballRb.velocity.magnitude * 2 + 1;
            }
            else 
            {
                var emission = smashParticles.emission;
                emission.rateOverTime = 0;
            }
    	}
    }
    
    private void OnCollisionEnter(Collision collision)
    {
    	if (collision.gameObject.tag == "Ball")
		{
            isTouchingBall = true;
            smashParticles.Play();

		    gameMgrScript.BallOnPlatform();
		}
    }

    private void OnCollisionExit(Collision collision)
    {
    	if (collision.gameObject.tag == "Ball")
		{
            isTouchingBall = false;
            smashParticles.Stop();
		}
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

			// Left Touch
            if (touch.position.x < Screen.width / 2)
            {
            	touchAxis = -1;
            }
            // Right Touch
            else
            {
            	touchAxis = 1;
            }
        }
        else
        {
            // No touch
            touchAxis = 0;
        }
    }
}
