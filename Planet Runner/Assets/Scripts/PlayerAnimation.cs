using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private float rotation;
    private bool isGrounded;
    private bool isRunning;

    private void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        rotation = playerController.Rotation;
        isGrounded = playerController.IsGrounded;
        SetAnimationParameters();
    }

    private void SetAnimationParameters()
    {
        if (isGrounded)
        {
            animator.ResetTrigger("jump");

            animator.SetFloat("rotation", rotation);
        }
        else
        {
            animator.ResetTrigger("landing");
        }
    }

    public void SetJumpTrigger()
    {
        animator.SetTrigger("jump");
    }

    public void SetLandingTrigger()
    {
        animator.SetTrigger("landing");
    }
}
