using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                playerMove.ActivateTrap();
            }
        }
    }
}
