using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] float xClampRange = 8.5f;
    [SerializeField] float zClampRange = 4.5f;
    Vector2 movement;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() // Hareketi FixedUpdate içinde yapmak daha stabil olmasını sağlar
    {
        MovePlayer();
    }

    public void Move (InputAction.CallbackContext context) // Hareket inputu
    {
        movement = context.ReadValue<Vector2>(); 
        

    }
    private void MovePlayer() // Move player
    {
        Vector3 currentPos = GetComponent<Rigidbody>().position; // Mevcut pozisyon
        Vector3 move = new Vector3(movement.x, 0, movement.y); // Hareket
        Vector3 newPos = currentPos + move * (moveSpeed * Time.fixedDeltaTime); // Yeni pozisyon
        newPos.x = Mathf.Clamp(newPos.x, -xClampRange, xClampRange); // X pozisyonunu sınırla 
        newPos.z = Mathf.Clamp(newPos.z, -zClampRange, zClampRange); // Z pozisyonunu sınırla

        rb.MovePosition(newPos);
    }
}
