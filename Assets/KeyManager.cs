using System.Collections;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private bool isFollowing;
    public float followSpeed;
    public Transform followTarget;
    public GameObject Door;
    public Sprite blackColourSprite; // The new sprite
    private AudioSource audioSource;
    private bool keyPickedUp = false;

    // Adjustable follow duration in the Unity Inspector
    public float followDuration = 5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            if (!isFollowing)
            {
                PlayerMove thePlayer = FindObjectOfType<PlayerMove>();
                followTarget = thePlayer.keyFollowPoint;

                isFollowing = true;
                StartCoroutine(FollowPlayerForDuration());
            }
        }
        if (other.CompareTag("player") && !keyPickedUp)
        {
            Debug.Log("Key Picked up");
            Door.GetComponent<BoxCollider2D>().enabled = false;

            // play sound
            audioSource.Play();

            keyPickedUp = true;

            // Change the door sprite
            SpriteRenderer doorSpriteRenderer = Door.GetComponent<SpriteRenderer>();
            doorSpriteRenderer.sprite = blackColourSprite;

            // Adjust the sprite to the size of the collider
            BoxCollider2D doorCollider = Door.GetComponent<BoxCollider2D>();
            Vector2 colliderSize = doorCollider.size;
            doorSpriteRenderer.gameObject.transform.localScale = new Vector3(colliderSize.x / doorSpriteRenderer.sprite.bounds.size.x, colliderSize.y / doorSpriteRenderer.sprite.bounds.size.y, 1);

            // Optional: Disable the key's collider or the key itself
            GetComponent<Collider2D>().enabled = false; // disable collider
            // gameObject.SetActive(false); // disable key
        }
    }

    IEnumerator FollowPlayerForDuration()
    {
        yield return new WaitForSeconds(followDuration);

        isFollowing = false;
        GetComponent<Collider2D>().enabled = false; // disable key collider
        // gameObject.SetActive(false); // disable key
    }
}
