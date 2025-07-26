using UnityEditor;
using UnityEngine;

public class ChainLighting : MonoBehaviour
{

    public static int amountToChain;

    public static float damage;

    new CircleCollider2D coll;

    Animator animator;

    GameObject startObject, endObject;

    int singleSpawns = 1;
    
    ParticleSystem particle;

    GameManager gm;



    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (amountToChain == 0) Destroy(gameObject);

        coll = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        particle = GetComponent<ParticleSystem>();

        startObject = gameObject;

        singleSpawns = 1;

        gm = GameManager.instance;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemies") 
            && !collision.GetComponentInChildren<LightingStruck>())
        {

            if (singleSpawns == 0) return;

            singleSpawns--;

            amountToChain--;

            endObject = collision.gameObject;

            GameObject obj = Instantiate(gm.chainLightingEffect, collision.transform.position, Quaternion.identity);
            obj.transform.parent = collision.transform;
 
            Instantiate(gm.beenStruck, collision.transform);

            endObject.GetComponent<Enemy>().TakeDamage(damage, ElementalDamage.NONE);
         
            animator.StopPlayback();

            coll.enabled = false;

            particle.Play();

            var emitParams = new ParticleSystem.EmitParams();
            emitParams.position = startObject.transform.position;
            particle.Emit(emitParams, 1);

            emitParams.position = endObject.transform.position;
            particle.Emit(emitParams, 1);


            Destroy(gameObject, 1f);

        }
    }

    public void AnimationEnd()
    {
        Destroy(gameObject);
    }

}
