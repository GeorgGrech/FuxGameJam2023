using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform characterCanvas;


    [SerializeField] private Animator anim;
    private Vector3 lastPosition;

    private CharacterController controller;
    private float gravity = 9.8f;
    private float verticalSpeed = 0;

    [SerializeField] private float baseSpeed;
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float defendingSpeed;
    
    [SerializeField] private Transform characterBody; // Reference to the character's body.

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        lastPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        characterCanvas.rotation = Camera.main.transform.rotation;

        MovementInput();
        RotateCharacter();

        if (this.gameObject.transform.position == lastPosition)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        lastPosition = this.gameObject.transform.position;
    }

    void MovementInput()
    {   
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (controller.isGrounded)
        {
            verticalSpeed = 0; // grounded character has vSpeed = 0...
        }
        verticalSpeed -= gravity * Time.deltaTime;
        move.y = verticalSpeed;

        controller.Move(move * Time.deltaTime * movementSpeed);
    }

    void RotateCharacter()
    {
        // Get the mouse position in screen coordinates.
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position from screen space to world space.
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y - characterBody.position.y));

        // Calculate the look direction, ignoring the vertical (upward) component.
        Vector3 lookDirection = new Vector3(mousePosition.x - characterBody.position.x, 0, mousePosition.z - characterBody.position.z);

        // Ensure the character body rotates to face the mouse position.
        if (lookDirection != Vector3.zero)
        {
            characterBody.forward = lookDirection;
        }
    }


    public void DefendingSpeed()
    {
        movementSpeed = defendingSpeed;
    }

    public void ReturnToNormalSpeed()
    {
        movementSpeed = baseSpeed;
    }

}
