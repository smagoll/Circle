using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int lvl;
    public float damage;
    public float speedFlight; //скорость полета снаряда
    public GameObject bullet;
    public GameObject target;
    public GameObject mainCircle;

    public virtual void Start()
    {
        target = TargetEnemy();
    }

    void Update()
    {
        ProjectileFlight();
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)//попадание по врагу
    {
        if (collider.CompareTag("enemy"))
        {
            Destroy(gameObject); // уничтожение снаряда при попадании
        }
    }

    private GameObject TargetEnemy()//установка цели
    {
        GameObject spawner = GameObject.Find("Spawner");
        var scriptSpawner = spawner.GetComponent<Spawner>();
        return scriptSpawner.enemies[0];
    }

    void ProjectileFlight()//полет снаряда
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, target.transform.position, speedFlight * Time.deltaTime);
        }
    }
}

