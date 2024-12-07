using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scr_controlPlayer : MonoBehaviour
{
    public Rigidbody rb;

    public GameObject coinPrefab;

    private scr_AnimatorController animatorController;

    public float acceleration;
    public float deceleration;
    public float maxSpeed;
    public float runMultiplier;
    private float currentSpeed;
    public float jump = 5f;
    public bool isGrounded;
    public float coinSpawn = 0.5f;

    void Start()
    {
        animatorController = GetComponentInChildren<scr_AnimatorController>();

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        animatorController.UpdateSpeed(Mathf.Abs(currentSpeed));
    }

    public void HorizontalMove()
    {
        float targetDirection = Input.GetAxis("Horizontal");
        float targetSpeed;

        if (Input.GetKey(KeyCode.B) && isGrounded)
        {
            targetSpeed = maxSpeed * runMultiplier;
        }
        else
        {
            targetSpeed = maxSpeed * 0.5f;
        }

        if (targetDirection != 0)
        {
            if (Mathf.Sign(targetDirection) != Mathf.Sign(currentSpeed))
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.fixedDeltaTime);
            }
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetDirection * targetSpeed, acceleration * Time.fixedDeltaTime);

            Transform spriteTransform = transform.GetChild(0);
            Vector3 spriteScale = spriteTransform.localScale;
            spriteScale.x = Mathf.Abs(spriteScale.x) * Mathf.Sign(targetDirection);
            spriteTransform.localScale = spriteScale;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.fixedDeltaTime);
        }

        Vector3 move = new Vector3(currentSpeed, 0, 0);
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            Vector3 jumpDirection = new Vector3(currentSpeed, 0, 0);
            rb.AddForce(jumpDirection, ForceMode.Impulse);
            isGrounded = false;

            animatorController.PlayJumpAnimation();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
            isGrounded = true;
            animatorController.UpdateIsGrounded(isGrounded);
        }

        if (collision.gameObject.CompareTag("BoxCoin") && !isGrounded)
        {
            ContactPoint contact = collision.contacts[0];

            if (Vector3.Dot(contact.normal, Vector3.down) > 0.5f)
            {
                Vector3 spawnPosition = transform.position + Vector3.up * coinSpawn;

                GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

                Animator coinAnimator = coin.GetComponent<Animator>();
               
                Destroy(coin, 2f);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
            isGrounded = false;
            animatorController.UpdateIsGrounded(isGrounded);
        }
    }

}




