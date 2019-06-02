using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{


    [SerializeField]
    //Holds a reference to the player camera
    private Camera cam;

    [SerializeField]
    //Holds a reference to the players body
    private Rigidbody body;

    [SerializeField]
    //the player's movement speed
    private float moveSpeed = 10f;

    [SerializeField]
    //players mouse sensativity
    private float lookSense = 3f;

    [SerializeField]
    //Reflects whether or not a player is currently grappling
    private bool isGrappling = false;

    [SerializeField]
    //relfects whether or not a player is jumping
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        DoMovement();
        DoTurn();
    }

    //Update movement
    void DoMovement()
    {
        //get input values
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        //create the base vectors
        Vector3 _horMove = transform.right * _xMove;
        Vector3 _forMove = transform.forward * _zMove;
        //total up the vectors
        Vector3 _velocity = (_horMove + _forMove).normalized * moveSpeed;
        //Move as long as there is a velocity input
        if (_velocity != Vector3.zero)
        {
            body.MovePosition(transform.position + _velocity * Time.deltaTime);
        }        
    }

    //Update rotations (based on mouse) for the horizontal yaw, not pitch
    void DoTurn()
    {
        //use the yaw rotation to turn the body of the player
        float _yawRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yawRotation, 0f) * lookSense;
        body.MoveRotation(body.rotation * Quaternion.Euler(_rotation));
        
        if (cam != null)//can't rotate a camera that's not there
        {
            //use the pitch rotation to angle the camera
            float _pitchRotation = Input.GetAxisRaw("Mouse Y");

            Vector3 _camRotate = new Vector3(_pitchRotation, 0f, 0f) * lookSense;

            Vector3 _newCam = cam.transform.localRotation.eulerAngles - _camRotate;
            cam.transform.localEulerAngles = _newCam;
        }
    }

}
