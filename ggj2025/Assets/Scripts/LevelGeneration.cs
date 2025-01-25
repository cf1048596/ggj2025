using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPosition;
    public GameObject[] rooms;
    private int direction;
    public float moveAmount;
    private bool stopGeneration;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
    public void Start()
    {
        int randStartingPos = Random.Range(0, startingPosition.Length);
        transform.position = startingPosition[randStartingPos].position;
        Instantiate(rooms[Random.Range(0, rooms.Length)], transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwRoom <= 0&&stopGeneration==false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        { 
            timeBtwRoom -= Time.deltaTime;
        }
        
    }

    private void Move()
    {
        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX)
            {

                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
            } else
            {
                direction = 5;
            }

        }
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
            } else
            {
                direction = 5;
            }
        } else if (direction == 5) { 
            if (transform.position.y > minY) {
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
            transform.position = newPos;
            }
            else
            {
                stopGeneration = true;
            }
        }
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
        
    }
}
