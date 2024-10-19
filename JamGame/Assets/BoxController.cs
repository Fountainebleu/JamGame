using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private Rigidbody2D body;
    private int whereLook; //Показывает куда смотрит персонаж, если налево, то -1, если направо, то 1
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D boxCollider; // ко
    private GameObject cat;
    private GameObject[] humans;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        cat = GameObject.FindGameObjectWithTag("Cat");
        humans = GameObject.FindGameObjectsWithTag("Human");
        if (cat == null)
        {
            var f = true;
            foreach (var human in humans)
            {
                if (human.gameObject.GetComponent<NpcController>().isCollision)
                {
                    f = false;
                }
            }
            if (f)
            {
                Move();
                WhereCharLook(); 
            }
        }
    }

    private void Move() //Метод управляющий движением и ускорением на кнопку shift
    {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
    }
    private void WhereCharLook() //Меняет направление взгляда персонажа
    {
        if ((Input.GetAxis("Horizontal")) > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
            whereLook = 1;
        }
        else if ((Input.GetAxis("Horizontal")) < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
            whereLook = -1;
        }
    }
    
    private bool isGrounded() //Проверяет нахождение персонажа на земле
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
