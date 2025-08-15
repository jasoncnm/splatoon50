using UnityEngine;

public class mouse : MonoBehaviour
{

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3 (mousePos.x, mousePos.y);
    }
}
