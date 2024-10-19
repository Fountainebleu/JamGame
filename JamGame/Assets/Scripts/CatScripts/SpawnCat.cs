using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCat : MonoBehaviour
{
    public GameObject cat;
    private Rigidbody2D rb;

    private Collider2D boxCollider;

    [SerializeField] private LayerMask groundLayer;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<Collider2D>();
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

    private bool isGrounded() //Проверяет нахождение персонажа на земле
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.05f, groundLayer);
        return raycastHit.collider != null;
    }
}
