using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent KillEnemy = new();
    public static UnityEvent Gold = new();
    public static UnityEvent LoseGame = new();
    public static UnityEvent CountDown = new();
    public static UnityEvent spawn = new();
    public static UnityEvent HitEnemy = new();
    public static UnityEvent NextWave = new();
    public static UnityEvent UpCircle = new();
    public static UnityEvent<GameObject, bool> hideCircles = new();
    public static UnityEvent PressCircles = new();
    public static UnityEvent PressPlay = new();

    public static void BuySpawn()
    {
        Gold.Invoke();
    }

    public static void PlusGoldForKillEnemy()
    {
        KillEnemy.Invoke();
    }

    public static void StartLoseGameWindow()
    {
        LoseGame.Invoke();
    }

    public static void StartCountDown()
    {
        CountDown.Invoke();
        
    }

    public static void StartSpawn()
    {
        spawn.Invoke();
    }

    public static void StartHitEnemy()
    {
        HitEnemy.Invoke();
    }

    public static  void StartNextWave()
    {
        NextWave.Invoke();
    }

    public static void StartUpCircle()
    {
        UpCircle.Invoke();
    }

    public static void StartHideCircles(GameObject hideObject, bool status)
    {
        hideCircles.Invoke(hideObject, status);
    }

    public static void StartPressCircles()
    {
        PressCircles.Invoke();
    }

    public static void StartPressPlay()
    {
        PressPlay.Invoke();
    }
}
