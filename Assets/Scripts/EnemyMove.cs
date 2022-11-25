using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;
    public bool IsMove = true;
    public GameObject center;
    public GameObject spawnpoint;
    public float Angle = 0.13f;
    public static float Radius;

    private void Start()
    {
        center = GameObject.FindGameObjectWithTag("map");
        //spawnpoint = GameObject.Find("spawnEnemy");
        Radius = Vector2.Distance(center.transform.position, gameObject.transform.position);
    }
    public void Move(GameObject @object)
    {
        if (IsMove)
        {
            Angle += Time.deltaTime * speed; // меняется плавно значение угла

            var x = Mathf.Cos(Angle) * Radius;
            var y = Mathf.Sin(Angle) * Radius;
            @object.transform.position = new Vector3(x, y) + center.transform.position;
            
        }
    }
}

