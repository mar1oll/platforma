using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove;

    [Header("Moving")]
    public float walkingSpeed;
    public float runningSpeed;

    public float jumpForce;
    public float gravity;

    [Header("Rotation")]
    public Camera cameraMain;
    public float lookSpeed;
    public float lookXlimit;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    public float rotationX;

    public Animator animator;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        //Z axis
        float curSpeedZ = 0;
        
        if (canMove)
        {
            if (isRunning)
            {
                curSpeedZ = runningSpeed;
            }
            else
            {
                curSpeedZ = walkingSpeed;
            }

            curSpeedZ *= Input.GetAxis("Vertical");
        }
        else
        {
            curSpeedZ = 0;
        }

        //X axis
        float curSpeedX = 0;
        
        if (canMove)
        {
            if (isRunning)
            {
                curSpeedX = runningSpeed;
            }
            else
            {
                curSpeedX = walkingSpeed;
            }

            curSpeedX *= Input.GetAxis("Horizontal");
        }
        else
        {
            curSpeedX = 0;
        }

        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedZ) + (right * curSpeedX);

        //jump
        if (Input.GetKey(KeyCode.Space) && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXlimit, lookXlimit);
            cameraMain.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }   
        
    }
}
