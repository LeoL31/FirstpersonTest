using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float MouseSensitivity = 100f;
    [SerializeField] float MaxLookUp = 90f;
    [SerializeField] float MaxLookDown = -90f;
    [SerializeField] bool LockMouse = true;
    [SerializeField] Transform playerBody;
    


    private float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LockMouse == true) 
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, MaxLookDown, MaxLookUp);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
