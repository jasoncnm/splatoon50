using UnityEngine;

public class SplatterSpawner : MonoBehaviour
{
    [SerializeField] Transform splatter;

    public static SplatterSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    public void Spawn()
    {
        float spawnAngle = transform.rotation.eulerAngles.z;

        Quaternion rot = Quaternion.Euler(0, 0, spawnAngle - 90f);

        Transform tr = Instantiate(splatter, transform.position, rot);


    }

}
