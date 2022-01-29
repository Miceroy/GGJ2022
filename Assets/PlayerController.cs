using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    GameObject getGameObject(string tag)
    {
        GameObject go = GameObject.FindGameObjectWithTag(tag);
        Debug.AssertFormat(go, "Did not found game object of type: " + tag);
        return go;
    }

    GameController getGameController()
    {
        return getGameObject("GameController").GetComponent<GameController>();
    }


    PlayerInputs playerInputs;
    CharacterController characterController;

    public float speed;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 15.0f;
    public Transform cam;

    private bool makesAction = false;

    public bool isMakingAction()
    {
        return makesAction;
    }

    void Awake()
    {
        playerInputs = new PlayerInputs();
        characterController = GetComponent<CharacterController>();

        playerInputs.Gameplay.Movement.started += onMovementInput;
        playerInputs.Gameplay.Movement.canceled += onMovementInput;
        playerInputs.Gameplay.Movement.performed += onMovementInput;

        playerInputs.Gameplay.Action.started += context => makesAction = true;
        playerInputs.Gameplay.Action.performed += context => makesAction = true;
        playerInputs.Gameplay.Action.canceled += context => makesAction = false;
        playerInputs.Gameplay.Swap.performed += context => getGameController().swapPlayerCharacter();
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

    private void Start()
    {
        getGameController().register(this);
    }

    // Update is called once per frame
    void Update()
    {
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

