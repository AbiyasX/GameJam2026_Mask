using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Vision Settings")]
    public float viewAngle = 60f;
    public float viewDistance = 10f;
    public int rayCount = 30;
    public float eyeHeight = 0.2f;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float waypointTolerance = 0.3f;
    public bool playerSpotted;

    private int patrolIndex;
    private LineRenderer line;
    private NavMeshAgent agent;

    private enum State { Patrol, Chase }
    private State currentState = State.Patrol;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();


        player = GameObject.FindWithTag("Player").transform;

        line.loop = true;
        line.positionCount = rayCount + 2;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        var playerSystem = player.GetComponent<PlayerSystem>();

        bool canSee = CanSeePlayer() && playerSystem.canPlayerSpotted;
        DrawCone(canSee);
        if (canSee && !playerSystem.playerIsMasked && playerSystem.canPlayerSpotted)
        {
            playerSpotted = true;
            currentState = State.Chase;
        }

        if (!playerSystem.canPlayerSpotted)
        {
            playerSpotted = false;
            currentState = State.Patrol;
        }

        HandleState();
    }

    void HandleState()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                Chase();
                break;
        }
    }


    void Patrol()
    {
        agent.speed = patrolSpeed;

        if (!agent.pathPending && agent.remainingDistance <= waypointTolerance)
        {
            GoToNextPatrolPoint();
        }
    }

    void Chase()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.SetDestination(patrolPoints[patrolIndex].position);
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
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

        line.startColor = line.endColor = Color.red;
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

        if (Physics.Raycast(transform.position + Vector3.up * eyeHeight, dir, out RaycastHit hit, viewDistance))
        {
            if (!hit.transform.CompareTag("Player"))
                return false;
        }

        return true;
    }

    void GameOver()
    {
        Debug.Log("Game Over!! Playar Got Caught");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameOver();
        }
    }
}


