using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] private GameObject player; // Ссылка на игрока
    public bool isCollision = false;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private int whereLook; //Показывает куда смотрит персонаж, если налево, то -1, если направо, то 1
    [SerializeField] private LayerMask groundLayer;
    private Collider2D plcollider; // ко
    [SerializeField] private float xSize = 1;
    [SerializeField] private float ySize = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        plcollider = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            isCollision = true;
            Destroy(collision.gameObject);
        }
    }
    
    private void SpawnC()
    {
        Instantiate(player, new Vector2(rb.position.x - 1, rb.position.y), Quaternion.identity);
    }

    private void Update()
    {
        if (isCollision && Input.GetKeyDown(KeyCode.E))
        {
            isCollision = false;
            SpawnC();
        }
        
        else if (isCollision)
        {
            Move();
            Jump();
            WhereCharLook();
        }

        if (isCollision && Input.GetKeyDown(KeyCode.Q))
        {
            isCollision = false;
        }
    }
    
    private void Move() //Метод управляющий движением и ускорением на кнопку shift
    {
        if (Input.GetKey(KeyCode.LeftShift)) //Если нажата кнопка shift, то удвоит скорость
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * 2, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        }
    }

    private void Jump() //Метод прыжка
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    public static int whereIsHeLook = 1;
    private void WhereCharLook() //Меняет направление взгляда персонажа
    {
        if ((Input.GetAxis("Horizontal")) > 0)
        {
            transform.localScale = new Vector2(xSize, ySize);
            whereIsHeLook = 1;

        }
        else if ((Input.GetAxis("Horizontal")) < 0)
        {
            transform.localScale = new Vector2(-xSize, ySize);
            whereIsHeLook = -1;
        }
    }
    
    private bool isGrounded() //Проверяет нахождение персонажа на земле
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(plcollider.bounds.center, plcollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
