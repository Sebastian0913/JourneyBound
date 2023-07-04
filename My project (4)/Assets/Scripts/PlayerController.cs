using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Attributes
    [SerializeField] private Transform CheckGroundTransform = null;
    [SerializeField] private LayerMask playerMask;

    private readonly int SpeedParameter = Animator.StringToHash("Speed");
    private Vector3 initialPosition;                                                             // Store the initial position directly in the script

    private int score;
    private int lives;
    private Animator animator;
    private bool spaceKeyWasPressed;
    private bool isGrounded;
    private bool isJumping = false;
    //private bool hasFallen = false;                                                              // New flag to track if the player has fallen below the threshold
    private float horizontalInput;                                                           
    Rigidbody rigidBody;
    private float fallThreshold = -4f;
    private float previousVerticalVelocity;
    // Track previous vertical velocity to detect falling// Adjust this value to the appropriate fall threshold
    //private ArrayList[] inventory;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        initialPosition = transform.position;                                                    // Set the initial position to the player's current position                                              
        rigidBody = GetComponent<Rigidbody>();                                                   // gets the rigidBody of the object
    }

    // Update is called once per frame
    void Update()
    { 
        
        //checks if space key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)                                                     
        {
            spaceKeyWasPressed = true;
            //isGrounded = false;
        }

        // Get horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

    }


    // FixedUpdate is called after every physic update
    private void FixedUpdate()
    {

        isGrounded = Physics.OverlapSphere(CheckGroundTransform.position, 0.1f, playerMask).Length > 0;
        float currentVerticalVelocity = rigidBody.velocity.y;

        if (isGrounded)
        {
            rigidBody.velocity = new Vector3(horizontalInput * 1.5f, rigidBody.velocity.y, 0);
        }
        
        else
        {
            if (currentVerticalVelocity < previousVerticalVelocity)
            {
                if(transform.position.y < fallThreshold)
                {
                    RespawnPlayer();
                    return;
                }
            }
   
        }

        // Reset the hasFallen flag when the player is grounded again

        if (isGrounded)
        {
            previousVerticalVelocity = 0f;
        }
        else
        {
            previousVerticalVelocity = currentVerticalVelocity;
        }

        // TODO:
        // Doublejump adding a  counter 



        //checks if space key is pressed
        if (spaceKeyWasPressed == true)
        {
            //makes the player jump
            rigidBody.AddForce(Vector3.up * 6, ForceMode.VelocityChange);
            spaceKeyWasPressed = false;
            isJumping = true;
        }

        animator.SetFloat(SpeedParameter, Mathf.Abs(horizontalInput));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    private void RespawnPlayer()
    {
        
        transform.position = initialPosition;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        isGrounded = true;
        isJumping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);

            score++;
            //inventory.add(coin);

        }

        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);

            lives++;

        }
    }


}
