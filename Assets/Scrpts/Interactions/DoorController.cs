using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{

    [SerializeField] private bool randomDoorOnStart = true;
    [SerializeField] private int randomDoorOnStartChancePercentage = 50;
    [SerializeField] private bool IsLocked = false;

    private Animator doorAnimation;
    private bool isOpen;
    private bool isMoving = true; // checking if the Animations is Playing so you cant spamm the door open and close

    private float randomNummer;

    private void Awake()
    {
        doorAnimation = GetComponent<Animator>();
    }
    private void Start()
    {
        if (randomDoorOnStart == true)
        {
            randomNummer = Random.Range(0, 100);

            // If the random number is < chance, open the door
            if (randomNummer < randomDoorOnStartChancePercentage)
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

