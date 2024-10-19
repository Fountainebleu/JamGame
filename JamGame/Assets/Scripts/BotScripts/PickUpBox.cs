using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPickUp : MonoBehaviour
{
    [SerializeField] private GameObject catBox;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float throwPower = 100;

    [SerializeField] private bool isCanThrow = false;

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
            rbcatbox.mass = 1;
            print("Коробку подняли");
            isCollision = false;
            isPickUp = true;
            Invoke("CanWeDownBox", 0.1f);
        } 

        if (isPickUp)
        {   
            PickUpBox();
        }

        if (isPickUp && Input.GetKeyDown(KeyCode.F) && cwdb && isCanThrow == false)
        {
            rbcatbox.mass = 150;
            print("Коробку опустили");
            PickDownBox();
            isPickUp = false;
            npickUp = true;
            cwdb = false;
        }

        if (isPickUp && Input.GetKeyDown(KeyCode.F) && cwdb && isCanThrow == true)
        {
            rbcatbox.mass = 150;
            print("Коробку бросили");
            ThrowBox();
            isPickUp = false;
            npickUp = true;
            cwdb = false;
        }

        if (isGrounded())
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

    private void ThrowBox()
    {
        rbcatbox.AddForce(new Vector2(NpcController.whereIsHeLook * 0.5f, 1) * throwPower * rbcatbox.mass, ForceMode2D.Impulse);
        print(NpcController.whereIsHeLook);
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
        RaycastHit2D hit = Physics2D.Raycast(rbcatbox.position, Vector2.left, 0.03f, groundLayer);

        if (hit == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private bool cwdb = false;
    private void CanWeDownBox()
    {
        cwdb = true;
    }
}
