using TMPro;
using UnityEngine;

public class DebugGameState : MonoBehaviour
{

    TextMeshProUGUI text;

    public GlobalGameStateSO gameGlobal;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameGlobal.gameState == GameState.GAME_COMBAT)
        {
            text.text = "Combating (Wave " + GameManager.instance.wave + ")";
        }
        else if (gameGlobal.gameState == GameState.GAME_COMBAT_END)
        {
            text.text = "Combat Ended \n Next Wave (N)";
        }
        else if (gameGlobal.gameState == GameState.GAME_START)
        {
            text.text = "In Menu";
        }
    }
}
