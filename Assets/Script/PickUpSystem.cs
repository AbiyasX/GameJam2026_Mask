using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSystem : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRange = 2f;
    public float sphereRadius = 0.5f;
    public LayerMask pickupLayer;

    [Header("References")]
    public Transform playerTransform;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new inputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Pickup.performed += OnPickupPerformed;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Pickup.performed -= OnPickupPerformed;
        inputActions.Player.Disable();
    }

    void Update()
    {
        DetectItem();
    }

    GameObject detectedItem = null;

    void DetectItem()
    {
        Vector3 forward = playerTransform.forward;
        forward.y = 0;
        forward.Normalize();

        Ray ray = new Ray(playerTransform.position + Vector3.up * 0.5f, forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, sphereRadius, out hit, pickupRange, pickupLayer))
        {
            detectedItem = hit.collider.gameObject;
            Debug.Log("Detected Item: " + detectedItem.name);
        }
        else
        {
            detectedItem = null;
        }
    }

    void OnPickupPerformed(InputAction.CallbackContext context)
    {
        if (detectedItem != null)
        {
            Pickup(detectedItem);
        }
    }

    void Pickup(GameObject item)
    {
        Debug.Log("Picked up: " + item.name);
        item.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (playerTransform)
        {
            Gizmos.color = Color.yellow;
            Vector3 forward = playerTransform.forward;
            forward.y = 0;
            forward.Normalize();
            Gizmos.DrawWireSphere(playerTransform.position + Vector3.up * 0.5f + forward * pickupRange, sphereRadius);
        }
    }
}