using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseProjectile : Projectile
{
    public GameObject selectedEnemy; // ���� �� �������� ���� �����

    public override void Start()
    {
        base.Start();
        var mainCircleScript = mainCircle.GetComponent<RiseCircle>();
        if (selectedEnemy == target)
        {
            mainCircleScript.countHits++;
            Debug.Log("selectedEnemy == target");
        }
        else
        {
            mainCircleScript.TargetEnemy = target;
            mainCircleScript.countHits = 0;
            Debug.Log("selectedEnemy != null");
        }
        damage += mainCircleScript.countHits * mainCircleScript.damageForStack;
    }

    public override void OnTriggerEnter2D(Collider2D collider)//��������� �� �����
    {
        if (collider.CompareTag("enemy"))
        {
            collider.GetComponent<Enemy>().hp -= damage;
        }
        base.OnTriggerEnter2D(collider);
    }

}
