using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    private Animator doorAnimation;
    private bool isOpen;

    private void Awake()
    {
        doorAnimation = GetComponent<Animator>();
    }

    public void Interact() 
    {
     if (!isOpen)
        {
            doorAnimation.Play("DoorOpen", 0, 0.0f);
            isOpen = true;
        }

        else
        {
            doorAnimation.Play("DoorClose", 0, 0.0f);
            isOpen = false;
        }
    }

}

