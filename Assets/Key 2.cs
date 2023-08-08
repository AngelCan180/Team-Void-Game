using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Door;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("player"))
        {
            Debug.Log("Key Picked up");

            Door.GetComponent<BoxCollider2D>().enabled = false; 
        }


    }
}
