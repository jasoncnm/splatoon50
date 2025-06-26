using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        GameManager.instance.AddScore();
        Destroy(gameObject);
    }
}
