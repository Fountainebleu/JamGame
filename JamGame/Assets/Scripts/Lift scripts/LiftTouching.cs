using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTouching : MonoBehaviour
{
    [SerializeField] private GameObject liftTouching;
    private bool isAgain = false;
    private void OnTriggerEnter2D()
    {
        if (!isAgain)
        {
            liftTouching.SetActive(true);
            isAgain = true;
        }
        
    }

    private void OnTriggerExit2D()
    {
        isAgain = false;
        liftTouching.SetActive(false);
    }
}
