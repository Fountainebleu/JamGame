using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTouching : MonoBehaviour
{
    [SerializeField] private GameObject liftTouching;
    private bool isAgain = false;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isAgain && !collider.gameObject.CompareTag("Cat"))
        {
            liftTouching.SetActive(true);
            isAgain = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (isAgain && !collider.gameObject.CompareTag("Cat"))
        {
            isAgain = false;
            liftTouching.SetActive(false);
        }
        
    }
}
