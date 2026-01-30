using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private InputSystem_Actions action;

    private Vector2 move;
    private Rigidbody rb;
    private void Awake()
    {
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        action.Enable();
        action.Player.Move.performed += Move_performed;
        action.Player.Move.canceled += Move_performed;
    }

    private void OnDisable()
    {
        action.Player.Move.performed -= Move_performed;
        action.Player.Move.canceled -= Move_performed;
        action.Disable();
    }
    private void Move_performed(InputAction.CallbackContext obj)
    {
        move = obj.ReadValue<Vector2>();
    }

    private void Update()
    {
        rb.linearVelocity = new Vector3(move.x, 0f, move.y) * speed;
    }
}
