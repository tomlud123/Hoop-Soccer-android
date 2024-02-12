using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 25)
        {
            rb.AddForce(new Vector2(rb.velocity.x*-0.2f,rb.velocity.y*-0.2f), ForceMode2D.Impulse);
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.rigidbody == null && rb.velocity.y<0.8)
        {
            if (transform.position.y > 1)
                rb.AddForce(new Vector2(0, -3f), ForceMode2D.Impulse);
            if (transform.position.y <- 1)
                rb.AddForce(new Vector2(0, 3f), ForceMode2D.Impulse);
        }
        else if (rb.velocity.magnitude > 16)
            rb.AddForce(new Vector2(0, 0.1f), ForceMode2D.Impulse);

    }

}
