using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jump;
    private float move;

    public Rigidbody2D rb;
    private bool isClimbing = false;
    private bool isTrapActive = false;
    private float trapTimer = 3f;

    private GameObject pickedLadder;

    // added code
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrapActive)
        {
            trapTimer -= Time.deltaTime;
            if (trapTimer <= 0f)
            {
                isTrapActive = false;
                trapTimer = 3f;
            }
            else
            {
                return; // Player cannot move while trap is active
            }
        }

        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        // added code
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            audioSource.Stop();
        }

        if (isClimbing && pickedLadder == null) // Only climb when not holding a ladder
        {
            float climbSpeed = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isClimbing)
            {
                DropLadder();
            }
            else
            {
                PickLadder();
            }
        }
    }

    public void ActivateTrap()
    {
        isTrapActive = true;
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Ladder")|| collision.gameObject.CompareTag("Rope"))
        {
            isClimbing = true;
        }
        else if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Obstacle"))
        {
            ActivateTrap();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder") || collision.gameObject.CompareTag("Rope"))
        {
            isClimbing = false;
            rb.gravityScale = 1f; // Restore normal gravity
        }
    }

    private void PickLadder()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ladder"))
            {
                pickedLadder = collider.gameObject;
                pickedLadder.transform.SetParent(transform);
                pickedLadder.transform.localPosition = new Vector3(0f, 1f, 0f);
                isClimbing = true;
                break;
            }
        }
    }

    private void DropLadder()
    {
        if (pickedLadder != null)
        {
            pickedLadder.transform.SetParent(null);
            pickedLadder = null;
            isClimbing = true; // Enable climbing when ladder is dropped
            rb.gravityScale = 1f;
        }
    }
}
