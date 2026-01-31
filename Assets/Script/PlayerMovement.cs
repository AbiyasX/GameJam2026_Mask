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
    private Animator PlayerAnimator;
    private SpriteRenderer PlayerRenderer;
    private ParticleSystem maskVFX;

    private void Awake()
    {
        PlayerSystem = GetComponent<PlayerSystem>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerRenderer = GetComponentInChildren<SpriteRenderer>();
        maskVFX = GetComponentInChildren<ParticleSystem>();
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        action.Enable();

        action.Player.Move.performed += Move_performed;
        action.Player.Move.canceled += Move_performed;

        action.Player.Mask.performed += Mask_performed;

        action.Player.Sprint.performed += Sprint_performed;
        action.Player.Sprint.canceled += Sprint_canceled;
    }

    private void OnDisable()
    {
        action.Player.Move.performed -= Move_performed;
        action.Player.Move.canceled -= Move_performed;
        action.Player.Sprint.performed -= Sprint_performed;
        action.Player.Sprint.canceled -= Sprint_canceled;

        action.Disable();
    }

    private void Mask_performed(InputAction.CallbackContext obj)
    {
        PlayerSystem.playerIsMasked = !PlayerSystem.playerIsMasked;
        maskVFX.Play();
        PlayerAnimator.SetBool("IsMasked", (PlayerSystem.playerIsMasked));
    }
    private void Sprint_performed(InputAction.CallbackContext obj)
    {
        PlayerSystem.isSprinting = true;

        PlayerAnimator.SetFloat("AnimationSpeed", 2);
    }

    private void Sprint_canceled(InputAction.CallbackContext obj)
    {
        PlayerSystem.isSprinting = false;

        PlayerAnimator.SetFloat("AnimationSpeed", 1);
    }
    private void Move_performed(InputAction.CallbackContext obj)
    {
        move = obj.ReadValue<Vector2>();
        if(move.x > 0)
        {
            PlayerRenderer.flipX = false;
        }
        else if(move.x < 0)
        {
            PlayerRenderer.flipX = true;
        }
        else
        {
            return;
        }
    }

    private void Update()
    {
        playerSpeed = PlayerSystem.Sprint() ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector3(move.x, 0f, move.y) * playerSpeed;

        PlayerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(move.magnitude));
    }
}
