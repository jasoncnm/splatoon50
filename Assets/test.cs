using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class test : MonoBehaviour
{
    [SerializeField] ParticleSystem p;

    Vector3 pos1, pos2;

    private void Start()
    {
        StartCoroutine(ts());
    }
    IEnumerator ts()
    {
        for (; ;)
        {

            p.Play();

            var emitParams = new ParticleSystem.EmitParams();
            emitParams.position = pos1;
            p.Emit(emitParams, 1);

            emitParams.position = pos2;
            p.Emit(emitParams, 1);

            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        pos1 = transform.GetChild(0).position;
        pos2 = transform.GetChild(1).position;
    }
}
