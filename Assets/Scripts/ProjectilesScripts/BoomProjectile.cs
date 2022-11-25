using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomProjectile : Projectile
{
    [SerializeField] private float radius;
    public override void OnTriggerEnter2D(Collider2D collider)//попадание по врагу
    {
        if (collider.CompareTag("enemy"))
        {
            float angleFirstEnemy = collider.GetComponent<EnemyMove>().Angle;
            GameObject spawner = GameObject.Find("Spawner");
            var scriptSpawner = spawner.GetComponent<Spawner>();
            var enemies = scriptSpawner.enemies;
            foreach (var enemy in enemies)
            {
                if (Math.Abs(angleFirstEnemy - enemy.GetComponent<EnemyMove>().Angle) < radius)
                {
                    enemy.GetComponent<Enemy>().hp -= damage;
                }
                else
                {
                    break;
                }
            }
        }
        base.OnTriggerEnter2D(collider);
    }
}
