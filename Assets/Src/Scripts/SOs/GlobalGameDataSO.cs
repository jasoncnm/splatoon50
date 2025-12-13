using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GlobalGameData", menuName = "SO/GlobalGameDataSO")]
public class GlobalGameStateSO : ScriptableObject
{
    // TODO: May need to reset state when quit
    public GameState gameState = GameState.GAME_START;

    public int experience = 0;
    public int money = 0;
    public int musicVolume = 50;
    public int SFXVolume = 50;
    public int playerHealth = 1;
    public float combatTime = 50f;

}
