using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private InputAction moveAction;

    public float speed;
    private float move;

    public Rigidbody2D rb;
    private bool isClimbing = false;
    private bool isTrapActive = false;
    private float trapTimer = 3f;

    private GameObject pickedLadder;
    private Animator animator;
    

    // Audio related
    private AudioSource moveAudioSource;
    private AudioSource climbAudioSource;
    private AudioSource trapAudioSource; // New AudioSource for trap sound
    public AudioClip climbingSound; // AudioClip for climbing sound
    public AudioClip trapSound; // New AudioClip for trap sound
    void OnEnable()
    {
        moveAction.Enable();
    }
    void OnDisable()
    {
        moveAction.Disable();
    }

    void Start()
    {
        // Assuming the first AudioSource is for movement, the second is for climbing, and the third is for trap sound
        AudioSource[] audioSources = GetComponents<AudioSource>();
        moveAudioSource = audioSources[0];
        climbAudioSource = audioSources[1];
        trapAudioSource = audioSources[2]; // Assigning the third AudioSource
        trapAudioSource.clip = trapSound; // Assigning the trap sound to the AudioSource
        animator = GetComponent < Animator > ();
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
        // Moving sound
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!moveAudioSource.isPlaying)
            {
                moveAudioSource.Play();
            }
        }
        else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            moveAudioSource.Stop();
        }

        // Climbing code
        if (isClimbing && pickedLadder == null)
        {
            float climbSpeed = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed * speed);

            if (!climbAudioSource.isPlaying)
            {
                climbAudioSource.clip = climbingSound;
                climbAudioSource.Play();
            }
            animator.SetBool("IsClimbing", true);
            Debug.Log("Hi");
        }
        else if (!isClimbing)
        {
            if (climbAudioSource.isPlaying)
            {
                climbAudioSource.Stop();
            }
           animator.SetBool("IsClimbing", false); 
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
    private void onPlayerMove()
    {
        //rb.velocity = new Vector2(speed * move, rb.velocity.y);
        rb.velocity = moveAction.ReadValue<Vector2>();
    }

    public void ActivateTrap()
    {
        isTrapActive = true;
        rb.velocity = Vector2.zero;

        if (!trapAudioSource.isPlaying)
        {
            trapAudioSource.Play();
            StartCoroutine(StopTrapSoundAfterDelay(3f)); // Stop the sound after 3 seconds
        }
    }

    IEnumerator StopTrapSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (trapAudioSource.isPlaying)
        {
            trapAudioSource.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Ladder") || collision.gameObject.CompareTag("Rope"))
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
                animator.SetBool("IsClimbing", true);
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
            animator.SetBool("IsClimbing", true);
            rb.gravityScale = 1f;
        }
    }
}
