using Unity.Cinemachine;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform PointA;
    public CinemachineCamera PlayerCam;
    public CinemachineCamera otherCam;
    public int PlayerValue;
    public int otherValue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = PointA.position;
            if (PlayerCam == null) return;
            PlayerCam.Priority.Value = PlayerValue;
            if (otherCam == null) return;
            otherCam.Priority.Value = otherValue;
        }
        
    }
}
