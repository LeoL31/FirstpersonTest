using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float playerDefaultSpeed = 8f;
    [SerializeField] float playerSneakingSpeed =4;
    [SerializeField] float playerRunningSpeed = 11f;
    [SerializeField] float playerMovmenAcceleration = 8f;
    [SerializeField] float jumpHeight = 3.0f;
    [SerializeField] float gravity = -9.81f;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;
    private float targetSpeed;

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        // Get input for movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Walking & Running & Sneaking
        Vector3 move = transform.right * x + transform.forward * z;
        targetSpeed = playerDefaultSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetSpeed = playerRunningSpeed; // Running
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            targetSpeed = playerSneakingSpeed; // Sneaking
        }
        else
        {
            targetSpeed = playerDefaultSpeed; // Walking
        }
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, playerMovmenAcceleration * Time.deltaTime);
        controller.Move(move * currentSpeed * Time.deltaTime);


        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
