
using UnityEngine;
using UnityEngine.SceneManagement;

public class Falltrigger : MonoBehaviour
{
    //public GameObject gj;
    //public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "Player") 
        {
            //gj.SetActive(false);
            //text.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
