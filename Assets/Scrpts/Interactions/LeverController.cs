using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class LeverController : MonoBehaviour, IInteractable
{

    [SerializeField] private bool isLocked = false;
    [SerializeField] private bool isOffOnStart = false;
    private bool isOn;
    private bool isMoving = false;
    
    private Animator leverAnimation;

    void Awake()
    {
        leverAnimation = GetComponent<Animator>();
    }

    void Start()
    {
        if (isOffOnStart == true)
        {
            leverAnimation.Play("LeverOff", 0, 0.0f);
            isOn = false;
        }
        else
        {
            leverAnimation.Play("LeverOn", 0, 0.0f);
            isOn = true;
        }
    }

    public void Interact()
    {
        if (isOn == true && isMoving == false && isLocked == false)
        {
            isMoving = true;
            leverAnimation.Play("LeverOff", 0, 0.0f);
        }
        else if (isOn == false && isMoving == false && isLocked == false)
        {
            isMoving = true;
            leverAnimation.Play("LeverOn", 0, 0.0f);
        }
    }



    // these Functions are called by the Animation Event
    public void LeverOnAniDone()
    {
        isOn = true;
        isMoving = false;
    }
    public void LeverOffAniDone()
    {
        isOn = false;
        isMoving = false;
    }
}
