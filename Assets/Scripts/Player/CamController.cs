using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float camSensitivity;
    [SerializeField] private Transform neck; // Transform para modificar el rotation de la camera
    [SerializeField] private float verticalUpClamp = 45; // limita la visibilidad en + 0;
    [SerializeField] private float verticalDownClamp = 45; // limita la visibilidad en - 0;
    private Quaternion nextRotation;
    // valores modificados por el script
    private float mouseX;
    private float mouseY;
    void Update()
    {
        GetMouseInput();
        CameraMove();
    }
    private void GetMouseInput()
    {
        mouseX = Input.GetAxis(GameConstants.MOUSE_X);
        mouseY = Input.GetAxis(GameConstants.MOUSE_Y);
    }
    private void CameraMove()
    {

        //Rotate the neck transform based on the input
        neck.transform.rotation *= Quaternion.AngleAxis(mouseX * camSensitivity, Vector3.up);

        #region Vertical Rotation
        neck.transform.rotation *= Quaternion.AngleAxis(-mouseY * camSensitivity, Vector3.right);

        var angles = neck.transform.localEulerAngles;
        angles.z = 0;

        var angle = neck.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 360 - verticalUpClamp)
        {
            angles.x = 360 - verticalUpClamp;
        }
        else if (angle < 180 && angle > verticalDownClamp)
        {
            angles.x = verticalDownClamp;
        }


        neck.transform.localEulerAngles = angles;
        #endregion


        nextRotation = Quaternion.Lerp(neck.transform.rotation, nextRotation, Time.deltaTime * camSensitivity);
        if(Input.GetKeyUp(GameConstants.KEY_FREE_VISION)){
            neck.transform.rotation = transform.rotation;
        }
        //Set the player rotation based on the look transform
        if (!Input.GetKey(GameConstants.KEY_FREE_VISION))
        {
            transform.rotation = Quaternion.Euler(0, neck.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        neck.transform.localEulerAngles = new Vector3(angles.x, 0, angles.z);
        }
        
        
    }
}
