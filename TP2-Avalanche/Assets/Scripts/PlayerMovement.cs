using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.81f * 3;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask cubeLayer;
    [SerializeField] Transform groundCheck;

    private float groundDistance = 0.3f;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance, cubeLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementVector = new Vector3(horizontalInput, 0, 0);
        float magnitude = Mathf.Clamp01(movementVector.magnitude) * speed;
        movementVector.Normalize();

        controller.Move(movementVector * magnitude * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
