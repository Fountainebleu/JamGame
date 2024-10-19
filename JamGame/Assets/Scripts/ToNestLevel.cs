using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class ToNestLevel : MonoBehaviour
{
    [SerializeField] string nextLevelName;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Application.LoadLevel(nextLevelName);
        }
        
    }
}
