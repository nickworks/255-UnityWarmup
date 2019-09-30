using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject camHandler;

    [Header("Character Speed")]
    public float walkSpeed = 4.0f;
    public float runSpeed = 8.0f;
    public float crouchSpeed = 3.0f;
    public float walkTransitionSpeed = 0.1f;
    public float runTransitionSpeed = 0.2f;
    public float crouchTransitionSpeed = 0.1f;
    public float bouncePrevention = -5f;

    [Header("Character Jump")]
    public float jumpSpeed = 8.0f;
    public int jumpLimiter = 0;
    public float crouchDownSpeed = 0.1f;
    public float gravity = 10.0f;

    [Header("Character Crouch Settings")]
    public float controllerDefaultSize = 2f;
    public float controllerCrouchSize = 1f;
    public float controllerDefaultHeight = 0f;
    public float controllerCrouchHeight = -0.5f;
    public float camDefaultHeight = 0.5f;
    public float camCrouchHeight = 0.25f;

    [HideInInspector]
    public float speed = 0f;
    [HideInInspector]
    public bool isJumping = false;
    [HideInInspector]
    public bool isGrounded = true;
    [HideInInspector]
    public bool isGroundedPrev = true;
    [HideInInspector]
    public bool isSprinting = false;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;

    private float yDirection = 0;
    private float jumpTimer = 0;

    private float controllerSize = 0;
    private float controllerHeight = 0f;
    private float camHeight = 0f;
    private float velocityY = 0.0f;

    private CharacterController playerController;

    void Start()
    {
        playerController = player.GetComponent<CharacterController>();

        controllerSize = controllerDefaultSize;
        controllerHeight = controllerDefaultHeight;
        camHeight = camDefaultHeight;
}

    void Update()
    {
        isGroundedPrev = isGrounded;
        isGrounded = playerController.isGrounded;

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);
        moveDirection = transform.TransformDirection(moveDirection);
        Mathf.Clamp(moveDirection.magnitude, 0, 1);

        if (Input.GetButton("Fire3")) isSprinting = true;
        else isSprinting = false;

        if (!isSprinting && moveDirection.magnitude > 0.1) speedFunction(walkSpeed, walkTransitionSpeed);
        else if (isSprinting && moveDirection.magnitude > 0.1) speedFunction(runSpeed, runTransitionSpeed);
        else speedFunction(0, 0.1f);

        moveDirection.x *= speed;
        moveDirection.z *= speed;

        if (isGrounded)
        {
            isJumping = false;
            if (Input.GetButton("Jump") && jumpTimer >= jumpLimiter)
            {
                yDirection = jumpSpeed;
                isJumping = true;
            }
            else
            {
                jumpTimer++;
                jumpTimer = Mathf.Clamp(jumpTimer, 0, jumpLimiter);
                yDirection = -bouncePrevention;
            }
        }
        else
        {
            yDirection -= gravity * Time.deltaTime;
        }
        moveDirection.y = yDirection;

        playerController.height = controllerSize;
        playerController.center = new Vector3(playerController.center.x, controllerHeight, playerController.center.z);
        camHandler.transform.localPosition = new Vector3(camHandler.transform.localPosition.x, camHeight, camHandler.transform.localPosition.z);
    }

    private void FixedUpdate()
    {
        playerController.Move(moveDirection * Time.deltaTime);
    }

    void speedFunction(float desiredSpeed, float transitionSpeed)
    {
        speed = Mathf.SmoothDamp(speed, desiredSpeed, ref velocityY, transitionSpeed);
        speed = Mathf.Clamp(speed, 0, desiredSpeed);
    }
}
