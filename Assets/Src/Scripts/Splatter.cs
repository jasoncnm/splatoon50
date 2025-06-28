using System.Collections;

using UnityEngine;

public class Splatter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(selfDestruct());
    }

    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
