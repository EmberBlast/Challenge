using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private Transform playerModel;
    [SerializeField] private Transform playerOrientation;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float playersHeight = 2;

    [Header("Jump settings")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float airMultiplier;

    private Rigidbody rb;

    //Movement variables
    private bool isGrounded = false;
    private bool grounded = true;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    //Jump variables
    private bool isJumping = false;
    private float startJumpHeight = 0;      
    private bool readyToJump = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playersHeight * 0.5f, ground);

        Debug.DrawLine(transform.position, Vector3.down * playersHeight * 0.5f);

        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
            Debug.Log("Player grounded");
        }
        else
        {
            rb.drag = 0;
            Debug.Log("Player not grounded");
        }    
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ControlJump();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
     
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    #region Movement Logic
    private void MovePlayer()
    {
        moveDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    #endregion

    #region Jump Logic
    private void Jump()
    {
        float jumpAmmount = jumpForce;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpAmmount, ForceMode.Impulse);
        isJumping = true;
        startJumpHeight = transform.position.y;
    }

    private void ControlJump()
    {
        if (isJumping)
        {
            float height = transform.position.y;
            if (height - startJumpHeight >= maxJumpHeight)
            {
                isJumping = false;
            }
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    #endregion
}
