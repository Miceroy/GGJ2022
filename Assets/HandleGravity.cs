using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandleGravity : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] private float groundedGravity = -0.05f;
    [SerializeField] private float gravity = -9.8f;
    Vector3 currentMovement;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleGravity();
        characterController.Move(currentMovement * Time.deltaTime);
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            //float groundedGravity = -.05f; //Character controllerin on pakko saada joku alaspäin vetävä arvo
            currentMovement.y = groundedGravity;
        }
        else
        {
            //float gravity = -9.8f;
            currentMovement.y += gravity;
        }


    }
}
