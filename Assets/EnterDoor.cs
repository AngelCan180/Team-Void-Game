using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
   private bool enterAllowed;

   private void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.gameObject.CompareTag("player"))
       {
         //sceneToLoad = "To be continued";
         enterAllowed = true;
       }
   }

    private void Update()
    {
       if  (enterAllowed)
       {
           SceneManager.LoadScene("To be continued");
       }
    }
}