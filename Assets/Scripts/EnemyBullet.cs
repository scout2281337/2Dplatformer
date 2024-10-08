using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log("������ �� ������");
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 6) 
        {
            Destroy(gameObject);
        }
    }
}
