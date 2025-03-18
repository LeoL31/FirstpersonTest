using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{

    [SerializeField] bool randomDoorOnStart = true;
    [SerializeField] bool IsLocked = false;

    private Animator doorAnimation;
    private bool isOpen;
    private bool isMoving = true; // checking if the Animations is Playing so you cant spamm the door open and close

    private void Awake()
    {
        doorAnimation = GetComponent<Animator>();
    }
    private void Start()
    {
        if (randomDoorOnStart == true)
        {
            isOpen = Random.Range(0, 2) == 0 ? false : true;
            if (isOpen)
            {
                doorAnimation.Play("DoorOpen", 0, 0.0f);
            }
            else
            {
                doorAnimation.Play("DoorClose", 0, 0.0f);
            }
        }
    }


    public void Interact() 
    {
     if (isOpen == true && isMoving == false && IsLocked == false)
        {
            isMoving = true;
            doorAnimation.Play("DoorClose", 0, 0.0f);
        }
        else if (isOpen == false && isMoving == false && IsLocked == false)
        {
            isMoving = true;
            doorAnimation.Play("DoorOpen", 0, 0.0f);
        }
    }

    

    // these Functions are called by the Animation Event
    public void DoorOpenAniDone() 
    {
        isOpen = true;
        isMoving = false;
    }
    public void DoorCloseAniDone()
    {
        isOpen = false;
        isMoving = false;
    }
}

