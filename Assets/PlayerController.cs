using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
<<<<<<< Updated upstream
    public bool isMakingAction()
    {
        return false;
    }

    // Start is called before the first frame update
    void Start()
=======
    PlayerInputs playerInputs;
    CharacterController characterController;

    public float speed;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 1.0f;
    
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
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }

    }

    void onMovementInput(InputAction.CallbackContext context) 
>>>>>>> Stashed changes
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
