using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CollidesWithBullet : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Bullet Hit Wall");
    }

}
