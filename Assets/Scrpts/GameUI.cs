using Unity.VisualScripting;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _optionsMenu;
    private bool _optionsMenuVisible;

    private void Start()
    {
        _optionsMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _optionsMenuVisible == false)
        {
            _optionsMenu.SetActive(true);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            _optionsMenuVisible = true;
            
        }
        else if (Input.GetKeyDown(KeyCode.Q) && _optionsMenuVisible == true) 
        {
            _optionsMenu.SetActive(false);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked; 
            UnityEngine.Cursor.visible = false;

            _optionsMenuVisible = false;
            
        }
    }
}
