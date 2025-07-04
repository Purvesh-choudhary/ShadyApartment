using UnityEngine;

public class ShadowPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ShadowPlatform"))
        {
            isGrounded = true;
        }
    }

    // Gizmos to show player movement and platform collision
    private void OnDrawGizmos()
    {
        if (isGrounded)
        {
            Gizmos.color = Color.blue; // When on the shadow platform
        }
        else
        {
            Gizmos.color = Color.red; // When in the air
        }

        // Show the position of the shadow player
        Gizmos.DrawSphere(transform.position, 0.2f);

        // Optionally, visualize the platform collision box if needed
        Gizmos.color = Color.green;
        Collider2D shadowPlatformCollider = GetComponent<Collider2D>();
        if (shadowPlatformCollider != null)
        {
            Gizmos.DrawWireCube(shadowPlatformCollider.bounds.center, shadowPlatformCollider.bounds.size);
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Finish")){
            Debug.Log($"LVL COMPLETE");
        }
    }

}
