using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{

    private PlayerInput playerInput;

    private InputAction jumpAction;
    private InputAction interactAction;

    

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        jumpAction = playerInput.actions.FindAction("Jump");
        interactAction = playerInput.actions.FindAction("Interact");
    }

    private void Update()
    {
        if (jumpAction.IsPressed())
        {
            Debug.Log("Jump");
        }   
        if (interactAction.IsPressed())
        {
            Debug.Log("Interact");
        }
    }
}


