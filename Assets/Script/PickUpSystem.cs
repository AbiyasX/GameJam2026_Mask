using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSystem : MonoBehaviour
{
    [Header("Pickup Settings")]
    public int itemID;
    public float pickupRange = 2f;
    public LayerMask pickupLayer;

    [Header("References")]
    public Transform playerTransform;
    [SerializeField] ItemManager itemManager;

    private InputSystem_Actions inputActions;
    private GameObject detectedItem = null;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Interact.performed += OnPickupPerformed;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnPickupPerformed;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        DetectItem();
    }

    void DetectItem()
    {
        Collider[] hits = Physics.OverlapSphere(playerTransform.position + Vector3.up * 0.5f, pickupRange, pickupLayer);
        if (hits.Length > 0)
        {
            detectedItem = hits[0].gameObject;
            Debug.Log("Detected Item: " + detectedItem.name);
        }
        else
        {
            detectedItem = null;
        }
    }

    private void OnPickupPerformed(InputAction.CallbackContext context)
    {
        
        Pickup(detectedItem);
        
    }

    void Pickup(GameObject item)
    {  
        ItemManager.Instance.CheckItem(itemID);
        //Debug.Log("Picked up: " + item.name);
        Destroy(item);
    }

    private void OnDrawGizmos()
    {
        if (playerTransform)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerTransform.position + Vector3.up * 0.5f, pickupRange);
  
            if (detectedItem != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(detectedItem.transform.position, 0.2f);
            }
        }
    }
}
