using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpForce = 5.0f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 3.0f;
    public LayerMask groundLayer;

    private CharacterController characterController;
    private Transform cameraTransform;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check if grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, characterController.height / 2f + 0.1f, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep the character grounded
        }

        // Calculate movement
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        // Apply gravity
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
            else
            {
                velocity.y = -2f; // Small value to keep the character grounded
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Combine movement and gravity
        Vector3 move = transform.right * moveX + transform.forward * moveZ + Vector3.up * velocity.y;
        characterController.Move(move * Time.deltaTime);

        // Look rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        Vector3 rotation = transform.localEulerAngles;
        rotation.y += mouseX;
        transform.localEulerAngles = rotation;

        Vector3 cameraRotation = cameraTransform.localEulerAngles;
        cameraRotation.x -= mouseY;
        cameraTransform.localEulerAngles = cameraRotation;
    }
}
