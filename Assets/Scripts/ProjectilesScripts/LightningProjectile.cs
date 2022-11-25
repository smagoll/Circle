using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : Projectile
{

    public override void OnTriggerEnter2D(Collider2D collider)//��������� �� �����
    {
        if (collider.CompareTag("enemy"))
        {
            int count;// ���������� ������ �� ������� ���� ����
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
