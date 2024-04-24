using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public LayerMask whatIsWall;


    public float wallRunforce, maxWallRunTime, maxWallSpeed;

    bool isWallRight, isWallLeft;

    bool isWallRunning;

    public float maxWallRunCameraTilt, wallRunCameraTilt;

    public Transform orientation;

    public Rigidbody rb; 

    public float walkingSpeed = 7.5f;

    public float runningSpeed = 11.5f;

    public float jumpSpeed = 8.0f;

    public float gravity = 20.0f;

    public Camera playerCamera;

    public float lookSpeed = 2.0f;

    public float lookXLimit = 45.0f;

   


    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetKeyDown(KeyCode.Space) && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        CheckForWall();

        WallRunInput();

        StartWallRun();
        
        StopWallRun();


        if (isWallLeft && !Input.GetKey(KeyCode.D) || isWallRight && !Input.GetKey(KeyCode.A))
        {
            moveDirection.y = jumpSpeed / 3;
        }

        if (isWallRight || isWallLeft && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            rb.AddForce(orientation.up * jumpSpeed * 5f);

        if (isWallLeft && Input.GetKey(KeyCode.A))
            rb.AddForce(-orientation.right * jumpSpeed * 3.2f);

        if (isWallRight && Input.GetKey(KeyCode.D))
            rb.AddForce(orientation.right * jumpSpeed * 3.2f);

        rb.AddForce(orientation.forward * jumpSpeed * 1f);


        
    }

    private void WallRunInput () 
    {
        if (Input.GetKey(KeyCode.D) && isWallRight) StartWallRun();
        if (Input.GetKey(KeyCode.A) && isWallLeft) StartWallRun();
    }

    private void StartWallRun()
    {
        rb.useGravity = false;

       

        isWallRunning = true; 

        if (rb.velocity.magnitude <= maxWallSpeed)
        {
            rb.AddForce(orientation.forward * wallRunforce * Time.deltaTime);

            if (isWallRight)
                rb.AddForce(orientation.right * wallRunforce / 5 * Time.deltaTime);
           else
                rb.AddForce(-orientation.right * wallRunforce / 5 * Time.deltaTime);
        }
    }
    private void StopWallRun()
    {
        rb.useGravity = true;

        isWallRunning = false;
    }
    
    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, whatIsWall);

        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, whatIsWall);

        if (!isWallLeft && !isWallRight) 
            StopWallRun();
    }
}