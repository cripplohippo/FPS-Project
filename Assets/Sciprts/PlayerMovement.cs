using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    //Ground Check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody rb; //player rigidbody
    private Vector2 moveInput; //WASD and arrow keys
    private bool isGrounded;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        CheckGround();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void OnJump()//action jump
    {
        if(isGrounded)
        rb.AddForce(new Vector3(0f, jumpForce, 0f),ForceMode.Impulse);
    }

    void OnMovement(InputValue value)//action movement
    {
        moveInput = value.Get<Vector2>();//read vector2: x=a/d y=s/w
    }

    void MovePlayer()
    {
        //convert 2d input to world space using player right/foward
        Vector3 direction = (transform.right*moveInput.x) + (transform.forward*moveInput.y);
        direction = direction.normalized;

        rb.linearVelocity = new Vector3(direction.x*moveSpeed, rb.linearVelocity.y, direction.z*moveSpeed);
    }

    void CheckGround()
    {
        if(groundCheck ==  null)
        {
            isGrounded = false;
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    }

}
