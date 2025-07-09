using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CollidesWithBullet : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Bullet Hit Wall");

        if (audio) audio.Play();

    }

}
