using UnityEngine;

public class Walking : MonoBehaviour {
  public AudioSource TileWalking;
  bool isPlaying = false;
  void Start () {
    var audioClip = Resources.Load<AudioClip> ("sound");
    TileWalking.clip = audioClip;
  }
  void Update () {
    if (Input.GetKeyUp (KeyCode.A) && !isPlaying) {
      TileWalking.Play ();
      isPlaying = true;
    }
    if (Input.GetKeyUp (KeyCode.D) && !isPlaying) {
      TileWalking.Play ();
      isPlaying = true;
    }
  }
}
