using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    private float rotation;
    private bool isGrounded;
    private bool isRunning;

    private void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        playerMovement = PlayerData.playerMovementComp;
    }

    private void Update()
    {
        rotation = playerMovement.Rotation;
        isGrounded = playerMovement.IsGrounded;
        isRunning = playerMovement.IsRunning;
        SetGlobalAnimationParameters();
    }

    private void SetGlobalAnimationParameters()
    {
        animator.SetFloat("rotation", rotation);
        animator.SetBool("is grounded", isGrounded);
        animator.SetBool("is running", isRunning);
    }

    public void StartJumpAnimation()
    {
        animator.SetTrigger("jump");

        // reset landing animation trigger
        animator.ResetTrigger("landing");
    }

    public void StopJumpAnimation()
    {
        animator.SetTrigger("landing");

        // reset jump animation trigger
        animator.ResetTrigger("jump");
    }
}