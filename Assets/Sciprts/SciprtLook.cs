using UnityEngine;
using UnityEngine.InputSystem;

public class SciprtLook : MonoBehaviour
{
    public float mouseSensitivity = 50f; //final sens used
    public Transform cam; //camera transform

    private float xRotation = 0f; //vertical rotation accumlator
    private Vector2 lookInput; //from Input Systeme (mouse delta)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //cursor lock to center
        Cursor.visible = false; //make cursor invisible
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>(); //read mouse delta (x,y)
    }
    
    void Update()
    {
        HandleMouseLook(); //apple look to every frame
    }

    void HandleMouseLook()
    {
        float mouseX = lookInput.x*mouseSensitivity*Time.deltaTime; //yaw
        float mouseY = lookInput.y*mouseSensitivity*Time.deltaTime; //pitch

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // limit vertical look
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //pitch on cam

        transform.Rotate(Vector3.up * mouseX); //yaw on player body (Y axis)
    }

}
