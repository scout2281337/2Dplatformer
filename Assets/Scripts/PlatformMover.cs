using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed;

    private Vector3 targetPoint;
    void Start()
    {
        targetPoint = pointB;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);


        if (Vector3.Distance(transform.position, targetPoint) < 0.1f) 
        {
            if ( targetPoint == pointB ) 
            {
                targetPoint = pointA;
            }
            else if ( targetPoint == pointA ) { targetPoint = pointB; }
        
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pointA, pointB);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player") 
        {
            collision.transform.parent = transform; 
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player") 
        { 
            collision.transform.parent = null; 
        }   
    }
}
