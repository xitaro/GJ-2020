using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;

public class BeatEmUpMovement_SinglePlayer : MonoBehaviour//NetworkBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator anim;
    private CharacterController controller;


    [Header("Logic")]
    //define what is the slope
    private Vector3 slopeNormal;
    private float verticalVelocity;
    private bool isGrounded;

    [Header("Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float speedX = 5;
    [SerializeField] private float speedY = 5;
    [SerializeField] private float gravity = 0.25f;
    [SerializeField] private float terminalVelocity = 5.0f;
    [SerializeField] private float jumpForce = 8.0f;
    Vector3 moveVector;

    [Header("Rotation")]
    private float rotateSpeed = 5;

    [Header("Ground Check Raycast")]
    [SerializeField] private float extremitiesOffset = 0.05f;
    [SerializeField] private float innerVerticalOffset = 0.25f;
    [SerializeField] private float distanceGrounded = 0.15f;
    [SerializeField] private float slopeThreshold = 0.55f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    //[Client]
    private void Update()
    {
       // if (!hasAuthority) { return; }

        //Look at which key the user is pressing, store it
        Vector3 inputVector = PoolInput();

        //Multiply the inputs with the speed, and switch Y & Z
         moveVector = new Vector3(inputVector.x, 0, inputVector.y);
         Rotate(moveVector);

        // Store it in a variable, so we don't call it more than once per frame
        isGrounded = Grounded();
        if (isGrounded)
        {
            // Apply slight gravity
            verticalVelocity = -1;

            // If spacebar, apply high negative gravity, and forget about the floor
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                //reset the slope normal
                slopeNormal = Vector3.up;
            }
        }
        else //if not on the floor
        {
            verticalVelocity -= gravity;
            //rese the slope normal
            slopeNormal = Vector3.up;

            // Clamp to match terminal velocity, if faster
            // It is in the fastest speed that it can technical fall
            if (verticalVelocity < -terminalVelocity)
                verticalVelocity = -terminalVelocity;
        }

        // Apply verticalVelocity to our movement vector
        moveVector.y = verticalVelocity;

        // If we're on the floor, angle our vector to match its curves
        if (slopeNormal != Vector3.up) moveVector = FollowFloor(moveVector);
    }

    private void FixedUpdate()
    {
        //Finaly move the controller, this also checks for collisions
        controller.Move(moveVector*speed * Time.deltaTime);
       
    }

    public void Rotate(Vector3 moveVector)
    {
        Quaternion rotation = Quaternion.LookRotation(moveVector);
        transform.rotation = rotation;
    }

    private Vector3 PoolInput()
    {
        Vector3 r = Vector3.zero;

        r.x = Input.GetAxisRaw("Horizontal");
        r.y = Input.GetAxisRaw("Vertical");
        return r.normalized;
    }

    private Vector3 FollowFloor(Vector3 moveVector)
    {
        Vector3 right = new Vector3(slopeNormal.y, -slopeNormal.x, 0).normalized;
        Vector3 forward = new Vector3(0, -slopeNormal.z, slopeNormal.y).normalized;
        return right * moveVector.x + forward * moveVector.z;
    }

    public bool Grounded() {

        //Is it currently jumping, or being affected by something that pushes up
        //is not grounded
        if (verticalVelocity > 0)
            return false;

        //Value in Y, where the raycast is going to start
        float yRay = (controller.bounds.center.y - (controller.height * 0.5f)) + innerVerticalOffset; // Bottom of the character controller

        //If the first one hits the floor, do a return, it says that, during that frame the character is grounded
        RaycastHit hit;

        //Mid
        if(Physics.Raycast(new Vector3(controller.bounds.center.x, yRay,controller.bounds.center.z), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            Debug.DrawRay(new Vector3(controller.bounds.center.x + yRay, controller.bounds.center.z), -Vector3.up * (innerVerticalOffset + distanceGrounded), Color.red);
            // If hit the floor, grab the normal of the floor
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }

        //Front-Right
        if (Physics.Raycast(new Vector3(controller.bounds.center.x + (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z + (controller.bounds.extents.z - extremitiesOffset)),
            -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }

        //Front-Left
        if (Physics.Raycast(new Vector3(controller.bounds.center.x - (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z + (controller.bounds.extents.z - extremitiesOffset)),
            -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }

        //Back-Right
        if (Physics.Raycast(new Vector3(controller.bounds.center.x + (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z - (controller.bounds.extents.z - extremitiesOffset)),
            -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }

        //Back-Left
        if (Physics.Raycast(new Vector3(controller.bounds.center.x - (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z - (controller.bounds.extents.z - extremitiesOffset)),
            -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }

        return false;

    }


}
