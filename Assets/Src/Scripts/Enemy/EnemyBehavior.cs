using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


// Enemy AI:
//  1. AI Data
//    - target 
//    - current target
//    - obstacles
//  2. Detector
//    - obstacle detector
//    - target detector. NOTE: Currently by designed enemy can always sees target.
//  3. Steering Behaviour 
//    - danger
//    - interest
//  4. Steering Solver


public class AIData
{
    // NOTE: Enemy has only one target (aka. Player) for now
    public Transform target;

    // NOTE: Store the references of obstacles around enemy
    public Collider2D[] obstacles;

    // NOTE: determind current target to follow.
    //       Based on your detector implemenetation, if this is null, eney does not follow anyone.
    public Transform currentTarget;
}

public static class Directions
{
    public static Vector2[] eightDirections =
    {
        new Vector2(0, 1),
        new Vector2(1, 1).normalized,
        new Vector2(1, 0),
        new Vector2(1, -1).normalized,
        new Vector2(0, -1),
        new Vector2(-1, -1).normalized,
        new Vector2(-1, 0),
        new Vector2(-1, 1).normalized
    };
}


public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] float obstacleDectionRadius = 2f;

    [SerializeField] float obstacleColliderThreshold = 0.6f;

    [SerializeField] float detectionDelay = 0.05f;

    [SerializeField] LayerMask obstacleDectionMask;

    [SerializeField] bool showDetectGizmos = false, showTargetGizmos = false, showDangerGizmos = true, showInterestGizmos = true;

    AIData data;

    float[] danger = new float[8];
    float[] interest = new float[8];

    private void Start()
    {
        data = new AIData();
        data.target = GameManager.instance.player;
        data.currentTarget = data.target;

        InvokeRepeating("Detect", 0, detectionDelay);
    }

    private void Update()
    {
        ContextSteering();
    }

    void ContextSteering()
    {
        Steering();

        GetComponent<Enemy>().Move(GetDirectionToMove());
    }


    Vector2 GetDirectionToMove()
    {
        for (int i = 0; i < interest.Length; i++)
        {
            interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
        }

        Vector2 outputDirection = Vector2.zero;
        for (int i = 0; i < Directions.eightDirections.Length; i++)
        {
            outputDirection += Directions.eightDirections[i] * interest[i];
        }
        
        Vector2 result = outputDirection.normalized;

        return result;

    }

    void ResetData()
    {
        Array.Clear(danger, 0, danger.Length);
        Array.Clear(interest,0, interest.Length);
    }

    void Steering()
    {
        ResetData();

        foreach (Collider2D collider in data.obstacles)
        {
            if (collider.transform.name == transform.name) continue;


            Vector2 directionToObstacle = collider.ClosestPoint(transform.position) - (Vector2)(transform.position);
            float distanceToObstale = directionToObstacle.magnitude;

            float weight = (distanceToObstale <= obstacleColliderThreshold) ? 1 :  (obstacleDectionRadius - distanceToObstale) / obstacleDectionRadius;

            for (int i = 0; i < Directions.eightDirections.Length; i++)
            {
                float result = Vector2.Dot(directionToObstacle.normalized, Directions.eightDirections[i]);

                float val = weight * result;

                if (val > danger[i]) danger[i] = val;

            }

            Vector2 directionToTarget = (data.currentTarget.position - transform.position);
            for (int i = 0; i < interest.Length; i++)
            {
                float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

                if (result > 0)
                {
                    float val = result;
                    if (val > interest[i])
                    {
                        interest[i] = val;
                    }
                }
            }
        }
    }

    void Detect()
    {
        data.obstacles = Physics2D.OverlapCircleAll(transform.position, obstacleDectionRadius, obstacleDectionMask);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        if (showDetectGizmos)
        {
            Gizmos.DrawWireSphere(transform.position, obstacleDectionRadius);

            if (data.obstacles != null)
            {
                Gizmos.color = Color.red;
                foreach (Collider2D collider in data.obstacles)
                {
                    if (collider.transform.position != transform.position)
                    {
                        Gizmos.DrawSphere(collider.bounds.center, 0.2f);
                    }
                }


            }
        }

        if (showDangerGizmos)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < danger.Length; i++)
            {
                Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * danger[i] * 2f);
            }
        }

        if (showInterestGizmos)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < danger.Length; i++)
            {
                Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interest[i] * 2f);
            }
        }

        if (showTargetGizmos)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, data.currentTarget.position);
        }
    }

}
