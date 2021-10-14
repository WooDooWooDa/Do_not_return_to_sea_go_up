using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.81f * 5;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask cubeLayer;
    [SerializeField] Transform groundCheck;

    public bool IsGrounded { get { return isGrounded; } }

    private float groundDistance = 0.4f;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isWalling = false;

    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance, cubeLayer);

        if (!isGrounded && velocity.y < 0)
        {
            velocity.y = -6f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementVector = new Vector3(horizontalInput, 0, 0);
        float magnitude = Mathf.Clamp01(movementVector.magnitude) * speed;
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
        animator.SetBool("isWalling", hit.normal.y < 0.1f);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
