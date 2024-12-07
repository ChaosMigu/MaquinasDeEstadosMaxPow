using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_gomba : MonoBehaviour
{
    public Animator animator;
    public scr_controlPlayer controlPlayerScr; 
    public Rigidbody rb;  
    public Collider enemyCollider;
    public float moveSpeed = 2f;  
    public float moveTime = 5f;  
    private bool movingRight = true; 
    private float moveTimer;    
    void Start()
    {
        if (controlPlayerScr == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                controlPlayerScr = player.GetComponent<scr_controlPlayer>();
            }
        }
        
        rb = GetComponent<Rigidbody>();
        moveTimer = moveTime;  
        enemyCollider = GetComponent<Collider>();
    }
    public void MoveEnemy()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            movingRight = !movingRight;  
            moveTimer = moveTime;  
        }

        float moveDirection = movingRight ? 1 : -1;
        rb.velocity = new Vector3(moveDirection * moveSpeed, rb.velocity.y, rb.velocity.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (controlPlayerScr != null)
            {          
                if (!controlPlayerScr.isGrounded)
                {                             
                    animator.SetBool("Gomba", true);
                    Destroy(gameObject, 0.8f);          
                }
            }
        }
    }
}
