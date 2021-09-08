using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharController : MonoBehaviour
{
    // componentes
    private CharacterController characterController;

    // valores modificados por el programador
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    // Camera Settings
    [SerializeField] private float camSensitivity;
    [SerializeField] private Transform neck; // Transform para modificar el rotation de la camera
    private Quaternion nextRotation;
    [SerializeField] private float verticalUpClamp; // limita la visibilidad en + 0;
    [SerializeField] private float verticalDownClamp; // limita la visibilidad en - 0;

    // valores modificados por el script
    private float vertical_axis;
    private float horizontal_axis;
    private float mouseX;
    private float mouseY;
    private float gravity;
    private float verticalSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gravity = GameConstants.PLAYERS_GRAVITY;
    }

    void Update()
    {
        GetInput();
        GetMouseInput();
        PlayerMove();
        CameraMove();
    }
    private void FixedUpdate()
    {
        PlayerGravity();
    }
    private void GetInput()
    {
        vertical_axis = Input.GetAxis(GameConstants.VERTICAL);
        horizontal_axis = Input.GetAxis(GameConstants.HORIZONTAL);
    }
    private void GetMouseInput()
    {
        mouseX = Input.GetAxis(GameConstants.MOUSE_X);
        mouseY = Input.GetAxis(GameConstants.MOUSE_Y);
    }
    private void PlayerMove()
    {
        Vector3 move = (transform.forward * vertical_axis * speed) + (transform.right * horizontal_axis * speed) + (transform.up * verticalSpeed);
        characterController.Move(move * Time.deltaTime);
    }
    private void PlayerGravity()
    {
        if (characterController.isGrounded)
        {
            verticalSpeed = -gravity * Time.fixedDeltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpForce;
            }
        }
        else
        {
            verticalSpeed -= gravity * Time.fixedDeltaTime;
        }
    }
    private void CameraMove()
    {

        #region Follow Transform Rotation

        //Rotate the Follow Target transform based on the input
        neck.transform.rotation *= Quaternion.AngleAxis(mouseX * camSensitivity, Vector3.up);

        #endregion

        #region Vertical Rotation
        neck.transform.rotation *= Quaternion.AngleAxis( - mouseY * camSensitivity, Vector3.right);

        var angles = neck.transform.localEulerAngles;
        angles.z = 0;

        var angle = neck.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 360 -verticalUpClamp)
        {
            angles.x = 360 -verticalUpClamp;
        }
        else if(angle < 180 && angle > verticalDownClamp)
        {
            angles.x = verticalDownClamp;
        }


        neck.transform.localEulerAngles = angles;
        #endregion

        
        nextRotation = Quaternion.Lerp(neck.transform.rotation, nextRotation, Time.deltaTime * camSensitivity);

        //Set the player rotation based on the look transform
        transform.rotation = Quaternion.Euler(0, neck.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        neck.transform.localEulerAngles = new Vector3(angles.x, 0, angles.z);
    }
}
