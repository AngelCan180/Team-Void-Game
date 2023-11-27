using UnityEngine;

public class EnterDoor : MonoBehaviour
{

   private void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.CompareTag("player"))
       {
         //sceneToLoad = "To be continued";
         SceneController.instance.NextLevel();
       }
   }


}
