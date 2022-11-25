using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : Projectile
{

    public override void OnTriggerEnter2D(Collider2D collider)//попадание по врагу
    {
        if (collider.CompareTag("enemy"))
        {
            int count;// количество врагов по которым идет урон
            GameObject spawner = GameObject.Find("Spawner");
            var scriptSpawner = spawner.GetComponent<Spawner>();
            var enemies = scriptSpawner.enemies;
            if (enemies.Length > lvl)
            {
                count = lvl;
            }
            else
            {
                count = enemies.Length;
            }
            for (int i = 0; i < count; i++)
            {
                enemies[i].GetComponent<Enemy>().hp -= damage;
            }
        }
        base.OnTriggerEnter2D(collider);
    }
}
