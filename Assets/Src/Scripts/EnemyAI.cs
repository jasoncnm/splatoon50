using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigator : MonoBehaviour
{
    public float chaseRange = 10f; // Distance at which enemy stops chasing

    Transform target;
    NavMeshAgent agent;
    public bool isChasing { get; private set; } = false;


    private void Start()
    {

        target = GameManager.instance.player;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        // Check if references are valid
        if (agent == null || target == null) return;

        // Calculate 2D distance (ignoring Z-axis)
        Vector2 enemyPos = transform.position;
        Vector2 targetPos = target.position;
        float distance = Vector2.Distance(enemyPos, targetPos);

        // Handle chasing state
        if (distance >= chaseRange)
        {
            StartChasing();
        }
        else if (isChasing)
        {
            StopChasing();
        }

        LookAtPlayer();

    }

    void LookAtPlayer()
    {
        Vector3 lookDir = target.position - transform.position;
        float angle = Util.GetAngleFromDirectionalVector(lookDir);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void StartChasing()
    {
        if (!isChasing)
        {
            isChasing = true;
            agent.isStopped = false; // Resume agent movement
        }
        agent.SetDestination(target.position);
    }

    private void StopChasing()
    {
        isChasing = false;
        agent.isStopped = true; // Freeze agent immediately
        agent.ResetPath(); // Clear current path
    }

    // Visualize chase range in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}