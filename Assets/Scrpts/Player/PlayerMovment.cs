using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovment : MonoBehaviour
{
    enum MovementState { Walking, Running, Crouching }

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float runSpeed = 11f;
    [SerializeField] private float crouchSpeed = 4f;
    [SerializeField] private float acceleration = 0.5f;
    private MovementState currentState;
    private float targetSpeed;
    private float currentSpeed;

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 400f;
    [SerializeField] private float staminaReductionSpeed = 5f;
    [SerializeField] private float staminaRegenSpeed = 3f;
    [SerializeField] private float regenDelay = 2f;
    private float stamina = 400f;
    private float regenTimer = 0f;                  

    [Header("Jumping & Gravity")]
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Setup")]
    public Slider staminaSlider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector3 move;
    private Vector2 moveAmount;


    // InputSystem References
    // This reference is set through the Inspector.
    [Header("Input Key Mapping")]
    public InputActionReference movementAction; // WASD movement
    public InputActionReference jumpAction;
    public InputActionReference runAction;
    public InputActionReference crouchAction;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentState = MovementState.Walking;
        currentSpeed = walkSpeed;

        //Stamina UI Setup
        staminaSlider.maxValue = 1;
        staminaSlider.gameObject.SetActive(true);
    }


    private void OnEnable()
    {
        movementAction.action.Enable();

        jumpAction.action.Enable();
        jumpAction.action.performed += OnJumpPerformed;


        runAction.action.Enable();// Enable the underlying action
        runAction.action.performed += OnRunPerformed;// Subscribe to the 'performed' event, which is fired when the action is triggered
        runAction.action.canceled += OnRunPerformed;// Subscribe to the 'canceled' event, which is fired when the action is triggered (for button release)

        crouchAction.action.Enable();
        crouchAction.action.performed += OnCrouchPerformed;
        crouchAction.action.canceled += OnCrouchPerformed;
    }

    private void OnDisable()
    {
        movementAction.action.Disable();

        jumpAction.action.Disable();
        jumpAction.action.performed -= OnJumpPerformed;

        runAction.action.Disable();
        runAction.action.performed -= OnRunPerformed;

        crouchAction.action.Disable();
        crouchAction.action.performed -= OnCrouchPerformed;
    }




    void OnInteractionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted(Not implemented yet! ;)");
    }

    //Case switching on Input Events
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
        if (isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void Update()
    {

        GroundCheck(); //Check if player is grounded
        Movment();//Player Horzontal Movment
        Stamina();//Stamina Drain & Regen Delay
        Gravity();//Apply Gravity
    }
    void GroundCheck() 
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
    }
    void Movment() 
    {

        if (currentState == MovementState.Running && moveAmount.y <= 0)// Force Walking if player is not moving forward
        {
            currentState = MovementState.Walking;
        }


        //Set speed based on state
        switch (currentState)
        {
            case MovementState.Walking:
                targetSpeed = walkSpeed;
                break;
            case MovementState.Running:
                targetSpeed = runSpeed;
                break;
            case MovementState.Crouching:
                targetSpeed = crouchSpeed;
                break;
        }

        moveAmount = movementAction.action.ReadValue<Vector2>();
        move = transform.right * moveAmount.x + transform.forward * moveAmount.y;

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime); // Smoothly change speed

        controller.Move(move.normalized * currentSpeed * Time.deltaTime);
    }
    void Stamina() 
    {
        staminaSlider.value = stamina / maxStamina;

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

        //Stamina UI
        if (stamina < maxStamina)
        {
            staminaSlider.gameObject.SetActive(true);
        }
        else
        {
            staminaSlider.gameObject.SetActive(false);
        }
    }
    void Gravity() 
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
