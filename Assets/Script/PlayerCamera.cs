using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Follow")]
    public Transform player;

    [Header("Isometric Offset")]
    public Vector3 playerOffset = new Vector3(0f, 0f, 0f);
    public Vector3 normalOffset = new Vector3(0f, 15f, -13f);
    public Vector3 zoomedOffset = new Vector3(0f, 10f -8f);
    [Header("Smoothness")]
    public float followSpeed = 10f;
    public bool isZoomed = false;

    public static PlayerCamera instance;

    private void Awake()
    {
        instance = this;
    }

    public void zoomInCamera(bool toggle)
    {
        isZoomed = toggle;
    }

    private void LateUpdate()
    {
        if (!player) return;
        playerOffset = isZoomed ? zoomedOffset : normalOffset;
        Vector3 targetPos = player.position + playerOffset;

        transform.position = Vector3.Lerp(transform.position,targetPos,followSpeed * Time.deltaTime);
    }
}
