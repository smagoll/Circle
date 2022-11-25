using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryProjectile : Projectile
{
    public override void OnTriggerEnter2D(Collider2D collider)//попадание по врагу
    {
        if (collider.CompareTag("enemy"))
        {
            collider.GetComponent<Enemy>().hp -= damage;
        }
        base.OnTriggerEnter2D(collider);
    }
}
