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
    
    public bool isPickUp = false;
    private bool npickUp = false;
    
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
            print("Коробку подняли");
            rbcatbox.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            isCollision = false;
            isPickUp = true;
            Invoke("CanWeDownBox", 0.1f);
        } 

        if (isPickUp)
        {   
            print("Коробка держится");
            PickUpBox();
        }

        if (isPickUp && Input.GetKeyDown(KeyCode.F) && cwdb)
        {
            print("Коробку опустили");
            PickDownBox();
            isPickUp = false;
            npickUp = true;
            cwdb = false;
            rbcatbox.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        
        

        if (isGrounded() && npickUp)
        {
            print("На земле");
            npickUp = false;
        }
    }

    private void PickUpBox()
    {
        rbcatbox.position = new Vector2(rbNPC.position.x, rbNPC.position.y + 1.02f);
    }

    private void PickDownBox()
    {
        rbcatbox.position = new Vector2(rbNPC.position.x, rbNPC.position.y);
        rbNPC.position = new Vector2(rbNPC.position.x, rbNPC.position.y + 1.2f);
    }

    private bool isCollision = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") && !isPickUp)
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

    private bool cwdb = false;
    private void CanWeDownBox()
    {
        cwdb = true;
    }
}
