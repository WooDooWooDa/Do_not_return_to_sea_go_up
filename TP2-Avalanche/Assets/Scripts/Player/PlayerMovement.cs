using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float baseSpeed = 12f;
    [SerializeField] float baseJumpHeight = 2f;
    [SerializeField] float baseFallingSpeed = -10f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask cubeLayer;
    [SerializeField] Transform groundCheck;

    public bool IsGrounded { get { return isGrounded; } }

    private float groundDistance = 0.45f;
    private Vector3 velocity;
    [SyncVar]
    private bool isGrounded;
    private bool isWalling = false;
    private bool canMove;

    private float currentSpeed;
    private float currentJumpHeight;
    private float currentFallingSpeed;

    private Animator animator;

    public void ResetToBase()
    {
        currentJumpHeight = baseJumpHeight;
        currentSpeed = baseSpeed;
        currentFallingSpeed = baseFallingSpeed;
    }

    public void ApplyJumpBoost()
    {
        currentJumpHeight = baseJumpHeight * 2f;
    }

    public void ApplySpeedBoost()
    {
        currentSpeed = baseSpeed * 1.5f;
    }

    public void ApplyFeatherFalling()
    {
        currentFallingSpeed = baseFallingSpeed * 0.5f;
    }

    public void ToggleMovement()
    {
        velocity.y = 0;
        canMove = !canMove;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        ResetToBase();
        canMove = true;
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        if (!canMove) {
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance, cubeLayer);
        CmdUpdateGrounded(isGrounded);

        if (!isGrounded && velocity.y < 0)
        {
            velocity.y = currentFallingSpeed;
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementVector = new Vector3(horizontalInput, 0, 0);
        float magnitude = Mathf.Clamp01(movementVector.magnitude) * currentSpeed;
        movementVector.Normalize();

        controller.Move(movementVector * magnitude * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //animator.SetBool("isWalling", hit.normal.y < 0.1f);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(currentJumpHeight * -2f * gravity);
    }

    private void CmdUpdateGrounded(bool grounded)
    {
        isGrounded = grounded;
    }
}
