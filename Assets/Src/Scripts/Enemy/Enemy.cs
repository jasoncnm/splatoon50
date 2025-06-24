using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gm;

    private void Awake()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    private void OnParticleCollision(GameObject other)
    {
        gm.AddScore();
        gameObject.SetActive(false);
    }
}
