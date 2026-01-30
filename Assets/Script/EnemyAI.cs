using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public NavMeshAgent agent;

    [Header("Vision")]
    public float viewDistance = 10f;
    public float viewAngle = 60f;
    public LayerMask obstacleMask;
    public LayerMask playerMask;

    private bool canSeePlayer;

    void Update()
    {
        DetectPlayer();

        if (canSeePlayer)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    void DetectPlayer()
    {
        canSeePlayer = false;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > viewDistance) return;
   
        if (Vector3.Angle(transform.forward, dirToPlayer) > viewAngle * 0.5f)
            return;

        if (Physics.Raycast(
            transform.position + Vector3.up * 1.5f,
            dirToPlayer,
            out RaycastHit hit,
            viewDistance,
            obstacleMask | playerMask))
        {
            if (((1 << hit.collider.gameObject.layer) & playerMask) != 0)
            {
                canSeePlayer = true;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = canSeePlayer ? Color.red : Color.green;
        Gizmos.DrawLine(transform.position + Vector3.up * 1.5f, player.position);
    }

}
