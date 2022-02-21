using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0, 200)]
    private float speed,maxSpeed,jumpPower;
    private float jumpDelay = 0.2f;
    private float canJump = 0f;
    private PlayerInput playerInput;
    Input input;
    private Rigidbody rigidbody;



    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
        input = new Input();
        input.Player.Enable();
        input.Player.Jump.performed += Jump;
    }

    void Update()
    {
        Vector2 inputVector = input.Player.Move.ReadValue<Vector2>();
        rigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
        Debug.Log(rigidbody.velocity.magnitude);
        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time > canJump)
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            canJump = Time.time + jumpDelay;
        }
    }
}
