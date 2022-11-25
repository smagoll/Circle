using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static float startHp = 10;
    public float Hp;
    public float hp
    {
        set
        {
            Hp = value;
            ChangeHp();
        }
        get { return Hp; }
    }
    public TextMeshProUGUI hpBar;

    public void UpdateHpBar()
    {
        hpBar.text = Math.Ceiling(hp).ToString();
    }

    public static void UpHpNextWave()
    {
        startHp += startHp * 0.1f;
    }

    private void ChangeHp()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
            GlobalEventManager.PlusGoldForKillEnemy();
            Menu.goldForGame += (0.1f * startHp);
            Menu.expForGame += (0.05f * startHp);
        }
        else
        {
            GlobalEventManager.StartHitEnemy();
        }
    }

}
