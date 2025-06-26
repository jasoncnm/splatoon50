using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigator : MonoBehaviour
{
    Transform target;

    NavMeshAgent agent;

    private void Awake()
    {
        target = GameManager.instance.player;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        agent.SetDestination(target.position);
    }
}