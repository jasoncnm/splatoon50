using Unity.VisualScripting;
using UnityEngine;

public class CombatEndMenu : MonoBehaviour
{

    public float aniSpeed = 4f;
    public AnimationCurve curve;

    public GlobalGameStateSO gameGlobal;
    Transform frame;

    GameManager gm;
    RectTransform rect;

    float openTime = 0f;
    float closeTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openTime = 0f;
        closeTime = 0f;

        gm = FindAnyObjectByType<GameManager>();
        frame = transform.Find("Frame");
        frame.gameObject.SetActive(false);
        rect = frame.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameGlobal.gameState == GameState.GAME_COMBAT_END)
        {
            openTime += Time.deltaTime * aniSpeed;
            if (openTime > 1f) 
            { 
                openTime = 1f;
            }

            float tVal = curve.Evaluate(openTime);
            // TODO menu popup animation
            frame.gameObject.SetActive(true);
            rect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, tVal);
            Cursor.visible = true;
        }
        else if (gameGlobal.gameState == GameState.GAME_COMBAT && frame.gameObject.activeSelf)
        {
            closeTime += Time.deltaTime * aniSpeed;
            if (closeTime < 0f)
            {
                closeTime = 0f;
                frame.gameObject.SetActive(false);
                return;
            }

            float tVal = curve.Evaluate(closeTime);

            rect.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, tVal);
            return;
        }

        Debug.Log(openTime);

    }

    public void NextWave()
    {
        Cursor.visible = false;
        closeTime = 0f;
        openTime = 0f;
        gm.SwitchState();
    }

}
