using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveDemo : MonoBehaviour
{
    private CharacterController controller;

    private float gravity = 9.8f;
    private float verticalSpeed = 0;
    [SerializeField] private float movementSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
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
}
