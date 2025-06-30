using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigator : MonoBehaviour
{
    public float chaseRange = 10f; // Distance at which enemy stops chasing

    Transform target;
    NavMeshAgent agent;

    Transform Aim;

    public bool isChasing { get; private set; } = false;


    private void Start()
    {

        target = GameManager.instance.player;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Aim = transform.Find("Aim");
    }

    private void Update()
    {
        // Check if references are valid
        if (agent == null || target == null) return;

        // Calculate 2D distance (ignoring Z-axis)
        Vector2 enemyPos = transform.position;
        Vector2 targetPos = target.position;
        float distance = Vector2.Distance(enemyPos, targetPos);

        bool playerIsDashing = GameManager.instance.player.GetComponent<PlayerController>()._Dashing;

        // Handle chasing state
        if (distance >= chaseRange)
        {
            StartChasing(playerIsDashing);
        }
        else if (isChasing)
        {
            StopChasing();
        }


        if (!playerIsDashing) LookAtPlayer();
    }

    void LookAtPlayer()
    {
        Vector3 lookDir = target.position - Aim.position;
        float targetAngle = Util.GetAngleFromDirectionalVector(lookDir);
        float vel = 0;
        float angle = Mathf.SmoothDampAngle(Aim.rotation.eulerAngles.z, targetAngle, ref vel, 0.1f);

        Aim.rotation = Quaternion.Euler(0, 0, targetAngle);

        if (isChasing) transform.GetComponent<AnimationController>().PlayMoveAnimation(lookDir.normalized);

    }

    private void StartChasing(bool playerIsDashing)
    {
        if (!isChasing)
        {
            isChasing = true;
            agent.isStopped = false; // Resume agent movement
        }

        if (playerIsDashing)
        {
            agent.SetDestination(target.GetComponent<PlayerController>().positionBeforeDash);
        }
        else
        {
            agent.SetDestination(target.position);
        }
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