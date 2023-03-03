using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("Move")]
    public float MoveSpeed;
    public float JumpForce;
    private Vector2 curMovementInput;
    private LayerMask GroundCheck;
    bool IsGrounded;





    [Header("Look")]
    public Transform CameraContainer;
    public float minXLook;
    public float maxXLook;
    private float CamCurXRot;
    public float LookSensitivity;

    private Vector2 mouseDelta;
    private Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {
        rb= GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Move();
    }
    void LateUpdate()
    {
        CameraLook();  
    }
    void Move()
    {
        Vector3 dir=transform.forward*curMovementInput.y+transform.right*curMovementInput.x;
        dir *= MoveSpeed;
        dir.y=rb.velocity.y;

        rb.velocity=dir;
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
    public void  OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase==InputActionPhase.Performed)
        {
            curMovementInput= context.ReadValue<Vector2>();
        }
        else if(context.phase==InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase==InputActionPhase.Started)
        {
            if(IsGrounded)
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }
    }
    bool isGrounded()
    {
        Ray[] rays = new Ray[4];
        {
            new Ray(transform.position+(transform.forward*0.2f),Vector3.down);
            new Ray(transform.position + (-transform.forward * 0.2f), Vector3.down);
            new Ray(transform.position + (transform.right * 0.2f), Vector3.down);
            new Ray(transform.position + (-transform.right * 0.2f), Vector3.down);
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }
}
