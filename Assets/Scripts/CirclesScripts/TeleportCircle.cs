using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCircle : Circle
{
    [SerializeField] private double chanceStart;
    [SerializeField] private double chance;
    [SerializeField] private double up;

    public override void SetOptionsProjectile()
    {
        base.SetOptionsProjectile();
        bullet.GetComponent<TeleportProjectile>().chance = chance;
    }

    public override void SetCharacteristic()
    {
        base.SetCharacteristic();
        if (level > 1)
        {
            chance = chanceStart + (up * level-1);
        }
        else
        {
            chance = chanceStart;
        }
    }
}
