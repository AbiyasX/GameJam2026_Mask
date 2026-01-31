using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float runSpeed = 10f;
    public float walkSpeed = 5f;

    private InputSystem_Actions action;

    private Vector2 move;
    private Rigidbody rb;

    private PlayerSystem PlayerSystem;
    private void Awake()
    {
        PlayerSystem = GetComponent<PlayerSystem>();
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        action.Enable();

        action.Player.Move.performed += Move_performed;
        action.Player.Move.canceled += Move_performed;

        action.Player.Mask.performed += Mask_performed;
        action.Player.Mask.canceled += Mask_canceled;

        action.Player.Sprint.performed += Sprint_performed;
        action.Player.Sprint.canceled += Sprint_canceled;
    }

    

    private void OnDisable()
    {
        action.Player.Move.performed -= Move_performed;
        action.Player.Move.canceled -= Move_performed;
        action.Player.Sprint.performed -= Sprint_performed;
        action.Player.Sprint.canceled -= Sprint_canceled;

        action.Player.Sprint.performed -= Sprint_performed;
        action.Player.Sprint.canceled -= Sprint_canceled;

        action.Disable();
    }

    private void Mask_canceled(InputAction.CallbackContext obj)
    {
        PlayerSystem.playerIsMasked = true;
    }

    private void Mask_performed(InputAction.CallbackContext obj)
    {
        PlayerSystem.playerIsMasked = false;
    }
    private void Sprint_performed(InputAction.CallbackContext obj)
    {
        PlayerSystem.isSprinting = true;
    }

    private void Sprint_canceled(InputAction.CallbackContext obj)
    {
        PlayerSystem.isSprinting = false;
    }
    private void Move_performed(InputAction.CallbackContext obj)
    {
        move = obj.ReadValue<Vector2>();
    }

    private void Update()
    {
        playerSpeed = PlayerSystem.Sprint() ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector3(move.x, 0f, move.y) * playerSpeed;
    }
}
