using UnityEngine;

public class Pehota : Enemy
{
    EnemyMove moving;
    public float speed = 0.2f;
    
    private void Start()
    {
        hp = startHp;
        moving = gameObject.AddComponent<EnemyMove>();
        moving.speed = speed;
        UpdateHpBar();
        GlobalEventManager.HitEnemy.AddListener(UpdateHpBar);
    }


    private void Update()
    {
        moving.Move(gameObject);
    }
}
