using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    Vector2 movement;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() 
    {
        MovePlayer();
    }

    public void Move (InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        

    }
    private void MovePlayer()
    {
       Vector3 currentPos = GetComponent<Rigidbody>().position;
       Vector3 move = new Vector3(movement.x, 0, movement.y);
       Vector3 newPos = currentPos + move * (moveSpeed * Time.fixedDeltaTime);

         rb.MovePosition(newPos);
    }
}
