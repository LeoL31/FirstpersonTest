using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
   public void Interact();
}

public class InteractionRaycast : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;
    [SerializeField] private KeyCode interactionKey = KeyCode.V;
    [SerializeField] private Image crosshair = null;

    [SerializeField] private bool debugRay = false;

    private Color detecionColor = Color.white;


    private const string interactbleTag = "Interactable";

    private void Update()
    {
        if (debugRay == true)
        {
            Debug.DrawRay(transform.position, transform.forward * interactRange, detecionColor);
        }

        crosshair.color = detecionColor;

        RaycastHit hitInfo;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, interactRange, mask))
        {
            if (hitInfo.collider.CompareTag(interactbleTag))
            {
                detecionColor = Color.green;
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObjekt))
                {
                    Debug.Log("Hit: " + hitInfo.collider.name);
                    if (Input.GetKeyDown(interactionKey))
                    {
                        interactObjekt.Interact();
                    }
                }
            }
        }
        else
        {
            detecionColor = Color.white;
        }
    }
}
