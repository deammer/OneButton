using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : MonoBehaviour {
    public Transform playerGibs;
    public float speed = 5f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private bool isGoingRight  = true;
    private bool isDead = false;
    private bool triggeredGameOver = false;

    private void Awake()
    {
        if (Random.value < .5f)
        {
            Flip();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Coliision");
        if (isDead) return;

        if (other.gameObject.tag == "Player")
        {
            // If jumped on, die
            if (PlayerController.instance.Velocity.y < -1f)
            {
                isDead = true;
                Destroy(gameObject);
            }
            else if (!triggeredGameOver)
            {
                triggeredGameOver = true;
                if (playerGibs)
                {
                    Transform gibs = Instantiate(playerGibs, transform.position, Quaternion.identity) as Transform;
                    gibs.SetParent(transform);
                    gibs.position = new Vector3(0, 0, 0);
                    gibs.localPosition = new Vector3(0, 0, 0);
                }
                GameManager.instance.GameOver();
            }
        }
    }
    private void Update()
    {
        Vector2 move = new Vector2(speed * Time.deltaTime, 0);
        if (!isGoingRight)
        {
            move.x *= -1;
        }
        transform.Translate(move);

        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, -Vector2.up, 1f, groundMask);
        if (hit.collider == null)
        {
            Flip();
        }

        Debug.DrawLine(groundCheck.position, groundCheck.position - Vector3.up, Color.yellow);
    }

    private void Flip()
    {
        isGoingRight = !isGoingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
