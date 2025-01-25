using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] objects;
    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
