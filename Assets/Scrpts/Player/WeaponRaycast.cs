using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public interface Damageable
{
    public void TakeDamage();
}



public class WeaponRaycast : MonoBehaviour
{

    [SerializeField] private Camera playerCamera;

    // InputSystem References
    // This reference is set through the Inspector.
    public InputActionReference fireAction;

    private const string enemyTag = "Enemy";


    void OnEnable()
    {
        fireAction.action.Enable();
        fireAction.action.performed += OnFirePerformed;
    }
    void OnDisable()
    {
        fireAction.action.Disable();
        fireAction.action.performed -= OnFirePerformed;
    }
    void OnFirePerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100))
            {
                if (hit.collider.CompareTag(enemyTag))
                {
                    if (hit.collider.gameObject.TryGetComponent(out Damageable interactObjekt))
                    {
                        Debug.Log("Hit: " + hit.collider.name);
                        interactObjekt.TakeDamage();
                    }
                }
            }
        }
    }
}
