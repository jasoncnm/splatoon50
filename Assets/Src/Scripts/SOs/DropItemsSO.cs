
using UnityEngine;

[CreateAssetMenu(fileName = "DropItems", menuName = "SO/DropItemsSO")]
public class DropItemsSO : ScriptableObject
{
    public Transform coin;

    public Transform[] pistolUpgrades;

    public Transform[] guns;

    public Transform chest;
}

