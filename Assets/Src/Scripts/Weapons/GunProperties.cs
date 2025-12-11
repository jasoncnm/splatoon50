using System;
using UnityEngine;

public enum ElementalDamage
{
    NONE,
    FIRE,
    POSION,
    ICE,
    LIGHTING,
}

public class GunProperties : MonoBehaviour
{

    public float fireDelay = 0.0f;

    public float fireRate = 0.1f;

    public float damage, pierce, fallOffDistance, crits;

    public float spread = 0.0f;

    public float reloadTime;

    public int bulletCapacity;

    public ElementalDamage elementalDamage = ElementalDamage.NONE;

}
