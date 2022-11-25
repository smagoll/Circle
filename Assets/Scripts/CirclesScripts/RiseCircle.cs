using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseCircle : Circle
{
    public GameObject TargetEnemy; // враг по которому идет атака
    public int countHits;
    public int damageForStack;

    public override void SetOptionsProjectile()
    {
        base.SetOptionsProjectile();

        bullet.GetComponent<RiseProjectile>().selectedEnemy = TargetEnemy;
        bullet.GetComponent<RiseProjectile>().damage = Convert.ToSingle(damageStart);
    }

    public override void SetCharacteristic()
    {
        base.SetCharacteristic();
        damageForStack = level;
    }


}
