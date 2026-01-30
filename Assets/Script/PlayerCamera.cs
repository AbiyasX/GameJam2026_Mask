using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Follow")]
    public Transform player;

    [Header("Isometric Offset (world space)")]
    public Vector3 offset = new Vector3(0f, 8f, -8f);

    [Header("Smoothness")]
    public float followSpeed = 10f;

    private void LateUpdate()
    {
        if (!player) return;

        Vector3 targetPos = player.position + offset;

        transform.position = Vector3.Lerp(transform.position,targetPos,followSpeed * Time.deltaTime);
    }
}
