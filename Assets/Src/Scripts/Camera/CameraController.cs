using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Vector2 min, max;

    float maxPlayerDist = 20f;

    public void SetCameraPosition(Vector3 playerPos)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float playerDist = Vector2.Distance((Vector2)playerPos, (Vector2)mousePos);

        Vector3 lookDir = ((Vector2)mousePos - (Vector2)playerPos).normalized;

        if (playerDist > maxPlayerDist)
        {
            mousePos = playerPos + lookDir * maxPlayerDist;
        }

        // Vector3 midPoint = Vector3.Lerp(playerPos, mousePos, 0.5f);

        Vector3 midPoint = playerPos;

        Vector3 target = new Vector3(midPoint.x, midPoint.y, transform.position.z);

        Vector3 cameraMoveDir = (target - transform.position).normalized;

        float distance = Vector3.Distance(target, transform.position);
        float cameraMoveSpeed = 10f;

        if (distance > 0)
        {
            Vector3 newCamPos = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
            float distAfterMove = Vector3.Distance(newCamPos, target);

            if (distAfterMove > distance)
            {
                newCamPos = target;
            }

            newCamPos.x = Mathf.Clamp(newCamPos.x, min.x, max.x);
            newCamPos.y = Mathf.Clamp(newCamPos.y, min.y, max.y);

            transform.position = newCamPos;
        }

    }
}
