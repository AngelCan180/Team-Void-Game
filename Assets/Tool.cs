using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    private bool isPickedUp = false;
    private GameObject playerObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (!isPickedUp)
            {
                playerObject = collision.gameObject;
                PlayerMove playerMove = playerObject.GetComponent<PlayerMove>();
                if (playerMove != null)
                {
                    playerMove.PickUpTool(gameObject);
                    isPickedUp = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (isPickedUp)
            {
                PlayerMove playerMove = playerObject.GetComponent<PlayerMove>();
                if (playerMove != null)
                {
                    playerMove.DropTool(gameObject);
                    isPickedUp = false;
                }
            }
        }
    }
}
