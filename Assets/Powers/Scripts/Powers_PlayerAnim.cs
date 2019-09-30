using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_PlayerAnim : MonoBehaviour
{
    public CharacterController playerController;
    public Powers_PlayerMovement playerMove;
    public Animator animator;
    private float speed = 0f;

    private bool isGrounded = true;
    private bool isGroundedPrev = true;
    private bool isJumping = false;

    void Update()
    {
        isGroundedPrev = isGrounded;

        speed = playerController.velocity.magnitude;
        isGrounded = playerMove.isGrounded;
        isJumping = playerMove.isJumping;

        animator.SetFloat("speed", speed);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isGroundedPrev", isGroundedPrev);
        animator.SetBool("isJumping", isJumping);
    }
}

