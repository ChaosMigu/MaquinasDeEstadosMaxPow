using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_AnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void UpdateSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }
    public void PlayJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }
    public void UpdateIsGrounded(bool grounded)
    {
        animator.SetBool("isGrounded", grounded);
    }

    internal void SetBool(string v, bool isGrounded)
    {
        throw new NotImplementedException();
    }
}
