using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Melee : Enemy
{

    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(float damage, ElementalDamage elementalDamage)
    {
        base.TakeDamage(damage, elementalDamage);
    }

    public override void Move(Vector2 direction)
    {
        base.Move(direction);
    }


    public override void Die()
    {
        base.Die();
    }

}
