using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCat : MonoBehaviour
{
    public GameObject cat;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnC();
        }
    }

    private void SpawnC()
    {
        Instantiate(cat, new Vector2(rb.position.x, rb.position.y + 1), Quaternion.identity);
    }
}
