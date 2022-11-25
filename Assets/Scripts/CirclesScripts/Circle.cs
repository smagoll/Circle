using System;
using TMPro;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public int id;
    private bool isHave;
    public GameObject parentCircle;
    public string nameCircle;
    [SerializeField] private GameObject gameObjectSpawnDrop;
    [SerializeField] private TextMeshProUGUI textLevel;
    public int level;
    public float damageStart;
    public float damage;
    public GameObject bullet;
    public float speedShotGame;
    private float SpeedShot
    {
        get
        {
            return speedShotGame;
        }
        set
        {
            speedShotReal = 1f / value * 20f;
            speedShotGame = value;
        }
    }
    public float speedShotReal;
    private float IsSpawn;
    public GameObject[] enemies;
    public bool IsShoot = true;

    void Start()
    {
        SetCharacteristic();
        name = $"{nameCircle}{level}";
        IsSpawn = speedShotReal;
        UpdateLevel();
    }

    void Update()
    {
        GetEnemies();
        if (enemies.Length > 0 && IsShoot)
        {
            SetOptionsProjectile();
            SpawnProjectile();
        }
        
    }

    public virtual void SetCharacteristic()// установка характеристик
    {
        foreach (var circle in StartMenu.circles)
        {
            if (circle.Name.ToLower() == nameCircle)
            {
                id = circle.Id;
                break;
            }
        }
        if (level > 1)
        {
            damage = Convert.ToSingle(damageStart * Math.Pow(2, level-1));
        }
        else
        {
            damage = damageStart;
        }

        speedShotReal = 1f / (speedShotGame + level * 2) * 20f;
    }

    public void SpawnProjectile()// спавн снаряда
    {
        if (IsSpawn < 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity, parent: gameObject.transform);
            IsSpawn = speedShotReal;
        }
        else
        {
            IsSpawn -= Time.deltaTime;
        }
    }

    public virtual void SetOptionsProjectile()// установка настроек снаряда
    {
        var bulletOptions = bullet.GetComponent<Projectile>();
        bulletOptions.damage = this.damage;
        bulletOptions.lvl = level;
        bulletOptions.mainCircle = gameObject;
    }

    private void GetEnemies()// получение массива врагов
    {
        GameObject spawner = GameObject.Find("Spawner");
        var scriptSpawner = spawner.GetComponent<Spawner>();
        enemies = scriptSpawner.enemies;
    }

    void UpdateLevel()// обновление текста левела
    {
        textLevel.text = level.ToString();
    }

}
