using UnityEngine;

public class doubletap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerScript>()!=null)
            {
                collision.gameObject.GetComponent<PlayerScript>().doubletapvar = true;
            }
            Destroy(gameObject);
        }
    }
}
