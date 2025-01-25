using Unity.VisualScripting;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public LayerMask whatisRoom;
    public LevelGeneration levelGen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatisRoom);
        if (roomDetection == null) 
        {
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            if (levelGen.stopGeneration)
            {
                Object.Destroy(gameObject);
            }
        }
    }
}
