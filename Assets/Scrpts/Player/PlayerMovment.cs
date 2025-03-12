using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovment : MonoBehaviour
{
    enum MovementState { Walking, Running, Crouching }

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float runSpeed = 11f;
    [SerializeField] private float crouchSpeed = 4f;
    [SerializeField] private float acceleration = 8f;
    private MovementState currentState;
    private float currentSpeed;

    [Header("Stamina Settings")]
     private float stamina = 400f;
    [SerializeField] private float maxStamina = 400f;
    [SerializeField] private float staminaReductionSpeed = 5f;
    [SerializeField] private float staminaRegenSpeed = 3f;
    [SerializeField] private float regenDelay = 2f; 
    private float regenTimer = 0f;                  

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


    // Input References
    // This reference is set through the Inspector.
    [Header("Input Key Mapping")]
    public InputActionReference interactionAction;
    public InputActionReference jumpAction;
    public InputActionReference runAction;
    public InputActionReference chroutchAction;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentState = MovementState.Walking;
        currentSpeed = walkSpeed;
    }


    private void OnEnable()
    {
        interactionAction.action.Enable();   
        interactionAction.action.performed += OnInteractionPerformed;
        jumpAction.action.Enable();
        jumpAction.action.performed += OnJumpPerformed;
        runAction.action.Enable();// Enable the underlying action
        runAction.action.performed += OnRunPerformed;// Subscribe to the 'performed' event, which is fired when the action is triggered
        runAction.action.canceled += OnRunPerformed;// Subscribe to the 'canceled' event, which is fired when the action is triggered (for button release)
        chroutchAction.action.Enable();
        chroutchAction.action.performed += OnCrouchPerformed;
        chroutchAction.action.canceled += OnCrouchPerformed;
    } 
    private void OnDisable()
    {
        interactionAction.action.Disable();
        interactionAction.action.performed -= OnInteractionPerformed;
        jumpAction.action.Disable();
        jumpAction.action.performed -= OnJumpPerformed;
        runAction.action.Disable();
        runAction.action.performed -= OnRunPerformed;
        chroutchAction.action.Disable();
        chroutchAction.action.performed -= OnCrouchPerformed;
    }



    //Case switching on Input Events
    void OnInteractionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted");
    }
    void OnRunPerformed(InputAction.CallbackContext context) 
    {
       if (context.performed && stamina > 8) currentState = MovementState.Running;
       if (context.canceled) currentState = MovementState.Walking;
    }
    void OnCrouchPerformed(InputAction.CallbackContext context) 
    {
        if (context.performed) currentState = MovementState.Crouching;
        if (context.canceled) currentState = MovementState.Walking;
    }
    void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Jump");
    }

    void Update()
    {
        //Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        //Set speed based on state
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

        //Move player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move.normalized * currentSpeed * Time.deltaTime);

        //Stamina Drain & Regen Delay
        if (currentState == MovementState.Running && move.magnitude > 0.1f)
        {
            stamina -= staminaReductionSpeed * Time.deltaTime;
            regenTimer = regenDelay;
        }
        else
        {
            if (regenTimer > 0f)
            {
                regenTimer -= Time.deltaTime;
            }
            else
            {
                stamina += staminaRegenSpeed * Time.deltaTime;
            }
        }
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        //Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
