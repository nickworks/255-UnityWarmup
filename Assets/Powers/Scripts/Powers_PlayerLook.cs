using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_PlayerLook : MonoBehaviour
{
    public Transform playerBody;
    public Powers_PlayerMovement playerMove;
    public float mouseSensitivity;
    public bool cursorLock = true;
    public float camLeanTransition = 0.1f;
    public float camLeanIntensity = 1f;
    float xAxisClamp = 0f;

    private float camLean = 0f;

    void Update()
    {
        if(cursorLock) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;

        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        float targetRotCam = transform.localRotation.eulerAngles.x;
        float targetRotBody = playerBody.rotation.eulerAngles.y;

        targetRotCam -= rotAmountY;
        targetRotBody += rotAmountX;

        if (xAxisClamp > 70){
            xAxisClamp = 70;
            targetRotCam = 70;
        }
        else if (xAxisClamp < -85){
            xAxisClamp = -85;
            targetRotCam = 275;
        }

        camLean = Mathf.Lerp(camLean, -Input.GetAxis("Horizontal"), camLeanTransition);
        camLean *= camLeanIntensity;
        camLean = Mathf.Clamp(camLean, -camLeanIntensity, camLeanIntensity);

        transform.localRotation = Quaternion.Euler(targetRotCam,transform.localRotation.y, camLean);
        playerBody.rotation = Quaternion.Euler(0,targetRotBody,0);
    }
}
