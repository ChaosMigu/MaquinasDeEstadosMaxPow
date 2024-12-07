using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpControl : MonoBehaviour
{
    public float jumpForce;
    public bool isJumping;

    public
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        isJumping = true;
        //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    public void StopJump()
    {
        isJumping = false;
    }
}
