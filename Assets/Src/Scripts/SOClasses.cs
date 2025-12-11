using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GlobalGameData", menuName = "SO/GlobalGameDataSO", order = 1)]
public class GlobalGameStateSO : ScriptableObject
{
    public GameState gameState = GameState.GAME_START;
    public int experience = 0;
    public int money = 0;
    public int musicVolume = 50;
    public int SFXVolume = 50;
    public int playerHealth = 1;
    public string startGunName = "Gun_Magnum";
}

