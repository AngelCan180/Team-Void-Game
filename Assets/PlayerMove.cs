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

    private GameObject currentTool; // The currently held tool

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

        if (isClimbing)
        {
            float climbSpeed = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed * speed);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTool == null)
            {
                PickUpTool();
            }
            else
            {
                DropTool();
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
        else if (collision.gameObject.CompareTag("Ladder"))
        {
            isClimbing = true;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
        else if (collision.gameObject.CompareTag("Trap"))
        {
            ActivateTrap();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = 1f; // Restore normal gravity
        }
    }

    private void PickUpTool()
    {
        GameObject nearestTool = FindNearestTool();
        if (nearestTool != null)
        {
            currentTool = nearestTool;
            currentTool.transform.SetParent(transform);
            currentTool.transform.localPosition = Vector3.zero;
            currentTool.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void DropTool(GameObject toolObject)
    {
        if (currentTool != null)
        {
            currentTool.transform.SetParent(null);
            currentTool.GetComponent<Rigidbody2D>().isKinematic = false;
            currentTool = null;
        }
    }

    private GameObject FindNearestTool()
    {
        GameObject[] tools = GameObject.FindGameObjectsWithTag("Tool");
        GameObject nearestTool = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject tool in tools)
        {
            float distance = Vector3.Distance(transform.position, tool.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTool = tool;
            }
        }

        return nearestTool;
    }
}
