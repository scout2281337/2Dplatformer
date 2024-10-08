using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1);
    }



    
    
        
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log("попали по игроку");
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 6) 
        {
            Destroy(gameObject);
        }
    }
}
