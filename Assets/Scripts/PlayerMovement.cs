using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;

    private float Move;

    public Rigidbody2D rb;

    public bool isJumping;

    private bool isFacingRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(speed * Move, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump));
        }

        if (!isFacingRight && Move > 0)
        {
            Flip();
        } else if (isFacingRight && Move < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = true;
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
