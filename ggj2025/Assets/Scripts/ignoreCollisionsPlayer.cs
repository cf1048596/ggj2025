using System.Collections.Generic;
using UnityEngine;

public class ignoreCollisionsPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameObject[] ignoreObjects;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null&& GetComponent<Collider2D>()!=false)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
