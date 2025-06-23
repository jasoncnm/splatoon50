using UnityEngine;

public class Util : MonoBehaviour
{
    public static Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static float GetAngleFromDirectionalVector(Vector3 dir)
    {
        Vector3 norm = dir.normalized;
        float angle = Mathf.Atan2(norm.y, norm.x) * Mathf.Rad2Deg;

        return angle;

    }

}
