using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputs playerInputs;
    CharacterController characterController;

    public float speed;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 15.0f;

    public bool isMakingAction()
    {
        return false;
    }
    void Awake()
    {
        playerInputs = new PlayerInputs();
        characterController = GetComponent<CharacterController>();

        playerInputs.Gameplay.Movement.started += onMovementInput;
        playerInputs.Gameplay.Movement.canceled += onMovementInput;
        playerInputs.Gameplay.Movement.performed += onMovementInput;
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f; //Character controllerin on pakko saada joku alaspäin vetävä arvo
            currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
        }


    }


    // Update is called once per frame
    void Update()
    {
        handleGravity();
        handleRotation();
        characterController.Move(currentMovement * speed * Time.deltaTime);
    }

    void OnEnable()
    {
        playerInputs.Gameplay.Enable();
    }

    void OnDisable()
    {
        playerInputs.Gameplay.Disable();
    }
}

