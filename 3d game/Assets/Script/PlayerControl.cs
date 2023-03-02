using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Transform CameraContainer;
    public float minXLook;
    public float maxXLook;
    private float CamCurXRot;
    public float LookSensitivity;

    private Vector2 mouseDelta;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        CameraLook();  
    }
    void CameraLook()
    {
        CamCurXRot += mouseDelta.y * LookSensitivity;
        CamCurXRot = Mathf.Clamp(CamCurXRot, minXLook, maxXLook);
        CameraContainer.localEulerAngles = new Vector3(-CamCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * LookSensitivity, 0);
    }
    public void  OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta += context.ReadValue<Vector2>();
    }
}
