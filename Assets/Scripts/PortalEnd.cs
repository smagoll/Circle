using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnd : MonoBehaviour
{
    private int health = 5;
    private int CountPastEnemies
    {
        set
        {
            countPastEnemies = value;
            DeleteHeart();
            if (countPastEnemies >= health)
            {
                Time.timeScale = 0;
                countPastEnemies = 0;
                GlobalEventManager.StartLoseGameWindow();
            }
        }
        get
        {
            return countPastEnemies;
        }

    }
    public int countPastEnemies = 0;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
            CountPastEnemies++;
        }
    }

    private void DeleteHeart()
    {
        var hearts = GameObject.FindGameObjectsWithTag("heart");
        if (hearts.Length > 0)
        {
            Destroy(hearts[0]);
        }
    }
}
