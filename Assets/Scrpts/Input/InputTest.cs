using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour    
{

    public InputActionReference interact;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        interact.action.Enable();
        interact.action.performed += Interact;
    }

    private void OnDisable()
    {
        interact.action.Disable();
        interact.action.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted");
    }
}
