using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour {

    public float speed;
    public bool player;

    private float reactionTime = 0f;

    Rigidbody2D body;
    Transform ball;
    Text controls;

    public void HideControls()
    {
        controls.enabled = false;
    }

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();

        ball = GameObject.FindGameObjectWithTag("Ball").transform;
        controls = GameObject.Find(name.Split(' ')[0] + " Controls").GetComponent<Text>();

        if (!player)
            reactionTime = Random.Range(0.05f, 1.3f);
        else
            Invoke("HideControls", 4f);
    }

    void FixedUpdate()
    {
        if (player)
            body.velocity = new Vector2(0f, Input.GetAxisRaw("Vertical_" + name.Replace(" Paddle", "")) * speed * Time.fixedDeltaTime * 100f);
        else
        {
            Vector2 distances = new Vector2(Mathf.Abs(ball.position.x - transform.position.x), ball.position.y - transform.position.y);

            if (distances.x <= Mathf.Abs(transform.position.x))
            {
                if (distances.y > ball.GetComponent<CircleCollider2D>().radius + reactionTime)
                    body.velocity = new Vector2(0f, speed * Time.fixedDeltaTime * 100f);
                else if (distances.y < -ball.GetComponent<CircleCollider2D>().radius - reactionTime)
                    body.velocity = new Vector2(0f, -speed * Time.fixedDeltaTime * 100f);
                else
                    body.velocity = Vector2.zero;
            }

            if (ball.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
                transform.position = new Vector2(transform.position.x, Mathf.MoveTowards(transform.position.y, ball.position.y, speed * Time.fixedDeltaTime));
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (!player)
        {
            if (col.gameObject.tag == "Ball")
                reactionTime = Random.Range(0.05f, 1.3f);
        }
    }
}