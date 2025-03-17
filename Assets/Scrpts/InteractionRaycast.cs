using UnityEngine;
using UnityEngine.UI;

public class InteractionRaycast : MonoBehaviour
{
    [SerializeField] private float rayDistance = 2f;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

    private DoorController raycastedObjekt;

    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactbleTag = "Interactable";

    private void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if(Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, mask))
        {
            if (hit.collider.CompareTag(interactbleTag))
            {
                if (!doOnce)
                {
                    raycastedObjekt = hit.collider.GetComponent<DoorController>();
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(interactionKey))
                {
                    raycastedObjekt.ChangeDoorState();
                }
            }
        }
        else
        {
            if (isCrosshairActive)
            {
                CrosshairChange(false);
            }
            doOnce = false;
        }
    }

    private void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }

}
