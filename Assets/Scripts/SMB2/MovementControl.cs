using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public float axis;
    public float currentSpeed;

    public float walkSpeed;
    public float runSpeed;
    public float airSpeed;

    public float acceleration;
    public float deceleration;

    public float airAcceleration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    public void Walk()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, walkSpeed * axis, acceleration * Time.deltaTime);
        //rb2d.velocity = new Vector2(currentSpeed, rb2d.velocity.y);
    }

    public void Run()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed * axis, acceleration * Time.deltaTime);
    }

    public void AirMovement()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, walkSpeed * axis, airAcceleration * Time.deltaTime);
    }


    public void Decelaration()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
    }
}
