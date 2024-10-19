using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPickUp : MonoBehaviour
{
    [SerializeField] private GameObject catBox;

    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rbNPC;

    private Rigidbody2D rbcatbox;
    private Collider2D colcatbox;
    
    private bool pickUp = false;
    
    void Awake()
    {
        rbNPC = GetComponent<Rigidbody2D>();
        rbcatbox = catBox.GetComponent<Rigidbody2D>();
        colcatbox = catBox.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollision && Input.GetKeyDown(KeyCode.F))
        {
            rbcatbox.constraints = RigidbodyConstraints2D.None;
            rbcatbox.constraints = RigidbodyConstraints2D.FreezeRotation;
            isCollision = false;
            pickUp = true;
        }

        else if (pickUp)
        {
            PickUpBox();
        }

        else if (pickUp && Input.GetKeyDown(KeyCode.F))
        {
            PickDownBox();
            pickUp = false;
        }

        if (isGrounded())
        {
            rbcatbox.constraints = RigidbodyConstraints2D.FreezeRotation;
            rbcatbox.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        
    }

    private void PickUpBox()
    {
        rbcatbox.position = new Vector2(rbNPC.position.x, rbNPC.position.y);
    }

    private void PickDownBox()
    {
        rbcatbox.position = new Vector2(rbNPC.position.x, rbNPC.position.y);
        rbNPC.position = new Vector2(rbNPC.position.x, rbNPC.position.y + 1);
    }

    private bool isCollision = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            isCollision = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            isCollision =  false;
        }
    }

    private bool isGrounded() //Проверяет нахождение персонажа на земле
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(colcatbox.bounds.center, colcatbox.bounds.size, 0, Vector2.down, 0.02f, groundLayer);
        return raycastHit.collider != null;
    }
}
