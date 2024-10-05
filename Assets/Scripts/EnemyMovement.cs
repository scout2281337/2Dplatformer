using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed;
    public Transform player;

    private Vector3 TargetPoint;
    private bool isChasing;

    void Start()
    {
        TargetPoint = pointB;
    }

    void Update()
    {
        if (isChasing) 
        {
            Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, 0).normalized;
            transform.position += direction * speed * Time.deltaTime;

        } 
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPoint, speed * Time.deltaTime);

            // Увеличим пороговое значение для проверки на малое расстояние
            if (Vector3.Distance(transform.position, TargetPoint) < 0.05f)
            {
                // Меняем цель, когда враг доезжает до текущей
                TargetPoint = TargetPoint == pointB ? pointA : pointB;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pointA, pointB);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            isChasing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "Player") 
        {
            isChasing = false; 
        }
    }
}
