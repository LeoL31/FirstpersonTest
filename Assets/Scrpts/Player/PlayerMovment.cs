using UnityEditor.ShaderGraph;
using UnityEngine;

//Forces the object to have a CharacterController component
[RequireComponent(typeof(CharacterController))]

public class PlayerMovment : MonoBehaviour
{
    enum MovementState { Walking, Running, Crouching }

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float runSpeed = 11f;
    [SerializeField] private float crouchSpeed = 4f;
    [SerializeField] private float acceleration = 8f;

    [Header("Jumping & Gravity")]
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Setup")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;
    private MovementState currentState;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentState = MovementState.Walking; 
        currentSpeed = walkSpeed;
    }


    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        // Handle Movement State Switching
        if (Input.GetKey(KeyCode.LeftShift)) currentState = MovementState.Running;
        else if (Input.GetKey(KeyCode.LeftControl)) currentState = MovementState.Crouching;
        else currentState = MovementState.Walking;

        // Set speed based on state
        switch (currentState)
        {
            case MovementState.Walking:
                currentSpeed = walkSpeed;
                break;
            case MovementState.Running:
                currentSpeed = runSpeed;
                break;
            case MovementState.Crouching:
                currentSpeed = crouchSpeed;
                break;
        }


        // Move player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        currentSpeed = Mathf.Lerp(currentSpeed, currentSpeed, acceleration * Time.deltaTime);
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Debug.Log("Current Speed: " + currentSpeed);


        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);



    }
}
