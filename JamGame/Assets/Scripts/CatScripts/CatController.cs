using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private float maxDistanceToGo = 4;
    private Rigidbody2D body;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private LayerMask boxLayer;
    private Collider2D catcollider; // ко
    public static CatController Instance;

    [SerializeField] private float xSize = 1;
    [SerializeField] private float ySize = 1;
    private GameObject box;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        body = GetComponent<Rigidbody2D>();
        catcollider = GetComponent<BoxCollider2D>();
        box = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
        WhereCharLook();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(gameObject, 0.1f);
        }
        if (Math.Abs(body.transform.position.x - box.transform.position.x) > maxDistanceToGo ||
            Math.Abs(body.transform.position.y - box.transform.position.y) > maxDistanceToGo)
        {
            Destroy(gameObject, 0.1f);
        }
    }

    private void Move() //Метод управляющий движением и ускорением на кнопку shift
    {
        if (Input.GetKey(KeyCode.LeftShift)) //Если нажата кнопка shift, то удвоит скорость
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * 2, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        }
    }

    private void Jump() //Метод прыжка
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }

    private void WhereCharLook() //Меняет направление взгляда персонажа
    {
        if ((Input.GetAxis("Horizontal")) > 0)
        {
            transform.localScale = new Vector2(xSize, ySize);
        }
        else if ((Input.GetAxis("Horizontal")) < 0)
        {
            transform.localScale = new Vector2(-xSize, ySize);
        }
    }
    
    private bool isGrounded() //Проверяет нахождение персонажа на земле
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(catcollider.bounds.center, catcollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D raycastHit2 = Physics2D.BoxCast(catcollider.bounds.center, catcollider.bounds.size, 0, Vector2.down, 0.1f, boxLayer);
        return raycastHit.collider != null || raycastHit2.collider != null;
    }
}
