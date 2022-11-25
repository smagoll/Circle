using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportProjectile : Projectile
{
    public double chance;

    public override void OnTriggerEnter2D(Collider2D collider)//попадание по врагу
    {
        if (collider.CompareTag("enemy"))
        {
            var rnd = Random.Range(0, 100);
            if (rnd <= chance)
            {
                if (collider.GetComponent<EnemyMove>().Angle < 0.5f)
                {
                    collider.GetComponent<EnemyMove>().Angle = 0.1f;
                }
                else
                {
                    collider.GetComponent<EnemyMove>().Angle -= 0.5f;
                }
            } 
        }
        base.OnTriggerEnter2D(collider);
    }
}
