using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int lvl;
    public float damage;
    public float speedFlight; //�������� ������ �������
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

    public virtual void OnTriggerEnter2D(Collider2D collider)//��������� �� �����
    {
        if (collider.CompareTag("enemy"))
        {
            Destroy(gameObject); // ����������� ������� ��� ���������
        }
    }

    private GameObject TargetEnemy()//��������� ����
    {
        GameObject spawner = GameObject.Find("Spawner");
        var scriptSpawner = spawner.GetComponent<Spawner>();
        return scriptSpawner.enemies[0];
    }

    void ProjectileFlight()//����� �������
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

