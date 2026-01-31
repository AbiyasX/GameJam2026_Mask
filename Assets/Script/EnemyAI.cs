using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Vision Settings")]
    public float viewAngle = 60f;
    public float viewDistance = 10f;
    public int rayCount = 30;
    public float eyeHeight = 0.2f;
    private LineRenderer line;
    private NavMeshAgent agent;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        line.loop = true;
        line.positionCount = rayCount + 2;
    }

    void Update()
    {
        bool canSee = CanSeePlayer();
        DrawCone(canSee);

        if (canSee)
            agent.SetDestination(player.position);
    }

    void DrawCone(bool canSee)
    {
        Vector3 origin = transform.position + Vector3.up * eyeHeight;
        line.SetPosition(0, origin);

        float halfAngle = viewAngle * 0.5f;

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = -halfAngle + (viewAngle / rayCount) * i;
            Quaternion rot = Quaternion.Euler(0, angle, 0);
            Vector3 dir = rot * transform.forward;

            line.SetPosition(i + 1, origin + dir * viewDistance);
        }

        line.startColor = line.endColor = canSee ? Color.green : Color.red;
    }

    bool CanSeePlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance > viewDistance)
            return false;
        Vector3 dir = toPlayer.normalized;

        float dot = Vector3.Dot(transform.forward, dir);
        float threshold = Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad);
        if (dot < threshold)
            return false;

        if (Physics.Raycast(transform.position + Vector3.up * 1.5f, dir, out RaycastHit hit, viewDistance))
        {
            if (!hit.transform.CompareTag("Player"))
                return false;
        }

        return true;
    }
}
