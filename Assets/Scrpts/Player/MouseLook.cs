using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse")]
    [SerializeField] float MouseXSensitivity = 100f;
    [SerializeField] float MouseYSensitivity = 100f;
    [SerializeField] float MaxLookDown = 90f;
    [SerializeField] float MaxLookUp = -90f;
    [Header("Setup")]
    [SerializeField] bool LockMouse = true;
    [SerializeField] Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        if (LockMouse == true) 
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseXSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseYSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, MaxLookDown, MaxLookUp);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
