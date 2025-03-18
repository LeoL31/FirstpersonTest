using UnityEngine;

public class Test : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
