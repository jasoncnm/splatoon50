using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public static EnemyBehavior instance;

    List<Enemy> enemies;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = new List<Enemy>();

        for (int i = 0; i < transform.childCount; i++)
        {
            enemies.Add(transform.GetChild(i).GetComponent<Enemy>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
