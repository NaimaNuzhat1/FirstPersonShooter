
using UnityEngine;
using UnityEngine.InputSystem;
public class MouseMovement : MonoBehaviour
{
    private PlayerMovement playerInput;

    float mouseSensitivity = 60f;
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update

    private void Awake()
    {

        playerInput = new PlayerMovement();
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
     
    
    }

    private void LateUpdate()
    {
        Vector2 cameraLook = playerInput.Touch.LookAround.ReadValue<Vector2>();
        float mouseX = cameraLook.x * mouseSensitivity * Time.deltaTime;
        float mouseY = cameraLook.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

    }
}
