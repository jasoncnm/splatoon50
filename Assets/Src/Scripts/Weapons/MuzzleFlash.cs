using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float lifeTime;

    float duration;

    public void Setup()
    {
        gameObject.SetActive(true);
        duration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime;

        if (duration > lifeTime)
        {
            gameObject.SetActive(false);
        }

    }
}
