using UnityEngine;

[CreateAssetMenu(fileName = "Guns", menuName = "SO/GunsSO")]
public class GunsSO : ScriptableObject
{

    [Range(0, 5)]
    public int selectedGunIndex;
    public Transform[] guns;
}

