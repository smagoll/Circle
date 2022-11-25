using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPointEnemy;
    public GameObject circle;
    public GameObject[] circles;
    public GameObject[] enemies;
    public GameObject[] spawnPoints;
    private GameObject[] allowPoints;
    public GameObject enemy;
    public Transform BattleField;
    public float speedSpawn = 1;
    public static bool checkWave = false;
    private int countEnemySpawn = 20;

    void Start()
    {
        allowPoints = spawnPoints;
        GlobalEventManager.spawn.AddListener(StartCorutineSpawnEnemy);
        GlobalEventManager.hideCircles.AddListener(HidingUnhidingCircleWhenConnected);
    }

    void Update()
    {
        FindObjectInCircle();
        if (checkWave)
        {

        }
        else
        {
            if (enemies.Length == 0)
            {

                GlobalEventManager.StartNextWave();
                GlobalEventManager.StartCountDown();
            }
        }
    }

    public void SpawnCircle()//спавн circle в рандомной точке карты
    {
        if (Menu.CheckGold())
        {
            allowPoints = spawnPoints.Where(x => x.GetComponent<SpawnPoint>().isBusy == false)
                            .ToArray();
            if (allowPoints.Length > 0 && allowPoints != null)
            {
                var rnd = Random.Range(0, allowPoints.Length - 1);
                var spawn = allowPoints[rnd];
                spawn.GetComponent<SpawnPoint>().isBusy = true;
                var circle = Menu.GetCircleForSpawn();
                var circleSpawn = Instantiate(circle, spawn.transform.position, Quaternion.identity, parent: null);
                circleSpawn.GetComponent<Circle>().IsShoot = true;
                circleSpawn.GetComponent<CanvasGroup>().blocksRaycasts = true;
                circleSpawn.GetComponent<CanvasGroup>().interactable = true;
                circleSpawn.GetComponent<BoxCollider2D>().enabled = true;
                GlobalEventManager.BuySpawn();
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    private void FindObjectInCircle()//поиск врагов по кругу
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy")
                            .OrderByDescending(x => x.GetComponent<EnemyMove>().Angle)
                            .ToArray();
    }

    private IEnumerator SpawnEnemy(GameObject enemyObject)//корутина спавна врагов
    {
        while (countEnemySpawn > 0)
        {
            Instantiate(enemyObject, spawnPointEnemy.position, Quaternion.identity, parent: BattleField);
            countEnemySpawn--;
            yield return new WaitForSeconds(speedSpawn);
        }
        checkWave = false;
    }

    void StartCorutineSpawnEnemy()//запуск корутины спавна врагов
    {
        StartCoroutine(SpawnEnemy(enemy));
    }

    private void HidingUnhidingCircleWhenConnected(GameObject connectedCircle, bool statusHiding)//скрытие и показывание circle, которые присоединяются
    {
        var conCircle = connectedCircle.GetComponent<Circle>();
        var circles = GameObject.FindGameObjectsWithTag("circle");
        var unconnectedCircles = circles.Where(x => x.GetComponent<Circle>().level != conCircle.level || x.GetComponent<Circle>().nameCircle != conCircle.nameCircle).ToArray();
        if (statusHiding)
        {
            foreach (var unconnectedCircle in unconnectedCircles)
            {
                //unconnectedCircle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
                unconnectedCircle.GetComponent<CanvasGroup>().alpha = 0.5f;
            }
        }
        else
        {
            foreach (var unconnectedCircle in unconnectedCircles)
            {
                //unconnectedCircle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                unconnectedCircle.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        
    }
}
