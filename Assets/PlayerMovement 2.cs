using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float climbTimer = 0.5f;
    public bool canClimb = false;
    public float climbSpeed = 3.0f;
    public float moveSpeed = 5.0f;

    private bool isTrapActive = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isTrapActive)
        {
            if (canClimb)
            {
                climbTimer -= Time.deltaTime;

                if (climbTimer <= 0)
                {
                    // Disable climbing ability
                    canClimb = false;
                }
                else
                {
                    // Climb up or down based on arrow key input
                    float verticalInput = Input.GetAxis("Vertical");
                    rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
                }
            }
            else
            {
                // Handle other player movements (left, right, etc.)
                float horizontalInput = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RedLadder"))
        {
            // Start climbing
            canClimb = true;
            climbTimer = 0.5f;
        }
        else if (collision.CompareTag("Trap"))
        {
            // Start trap effect
            isTrapActive = true;
            rb.velocity = Vector2.zero; // Stop player movement
            StartCoroutine(DisableTrapAfterDelay());
        }
    }

    private IEnumerator DisableTrapAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        isTrapActive = false;
    }
}
