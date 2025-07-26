using UnityEngine;

public class LightingStruck : MonoBehaviour
{
    [SerializeField, Range(0f,5f)] float nextStruckTime = 1.5f;
    private void Start()
    {
        Destroy(gameObject, nextStruckTime);
    }
}
