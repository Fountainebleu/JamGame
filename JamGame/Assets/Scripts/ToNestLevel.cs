using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class ToNestLevel : MonoBehaviour
{
    [SerializeField] string nextLevelName;

    [SerializeField] private GameObject liftTouching;
    private bool isAgain = false;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isAgain && collider.gameObject.CompareTag("Player"))
        {
            Application.LoadLevel(nextLevelName);
        }
        
    }
}
