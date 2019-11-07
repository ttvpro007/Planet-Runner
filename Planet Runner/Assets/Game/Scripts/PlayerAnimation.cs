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
        playerController = PlayerData.playerController;
    }

    private void Update()
    {
        rotation = playerController.Rotation;
        isGrounded = playerController.IsGrounded;
        isRunning = playerController.IsRunning;
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