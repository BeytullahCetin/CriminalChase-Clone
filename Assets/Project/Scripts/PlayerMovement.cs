using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [Range(1f,10f)]
    [SerializeField] private float moveSpeed = 5f;
    [Range(5f,20f)]
    [SerializeField] private float rotateSpeed = 10f;

    private Vector2 movementInput;
    private Vector3 moveVector;

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.forward = Vector3.Lerp(transform.forward, moveVector, rotateSpeed * Time.deltaTime);
        characterController.Move(moveVector * moveSpeed * Time.deltaTime);
    }

    public void GetMovementInput(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();

        moveVector.x = movementInput.x;
        moveVector.z = movementInput.y;

        moveVector.Normalize();
    }
}
