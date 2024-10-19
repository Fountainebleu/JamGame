using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotsAI : MonoBehaviour
{
    [SerializeField] private float seeDistance = 1f;
    [SerializeField] private float speed;
    
    private Rigidbody2D rb2D;
    private CapsuleCollider2D boxCollider;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private int directionOfMove = -1;

    private void Move()
    {
        rb2D.velocity = new Vector2(speed * directionOfMove, rb2D.velocity.y);

        if (IsWallLeft() || IsNoGroundAhead())
        {
            transform.localScale = new Vector2(-3f, 3.5f);
            directionOfMove = 1;
        }

        else if (IsWallRight() || IsNoGroundAhead())
        {
            transform.localScale = new Vector2(3f, 3.5f);
            directionOfMove = -1;
        }
    }

    private bool IsWallLeft()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb2D.position, Vector2.left, seeDistance, LayerMask.GetMask("Is Ground Ahead for AI"));

        if (hit == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private bool IsWallRight()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb2D.position, Vector2.right, seeDistance, LayerMask.GetMask("Is Ground Ahead for AI"));

        if (hit == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private bool IsNoGroundAhead()
    {
        RaycastHit2D hitright = Physics2D.Raycast(rb2D.position, Vector2.right, seeDistance, LayerMask.GetMask("Wall"));
        RaycastHit2D hitleft = Physics2D.Raycast(rb2D.position, Vector2.left, seeDistance, LayerMask.GetMask("Wall"));

        if (hitright == true || hitleft == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
