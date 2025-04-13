using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool isGrounded;

    public GameObject bulletPrefab;
    public Transform firePoint;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();

        if (Input.GetKeyDown(KeyCode.R)) // hoặc phím bạn muốn
        {
            Fire();
        }

    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // Lật mặt nhân vật theo hướng di chuyển
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1, 1);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        if (transform.localScale.x < 0)
        {
            // Quay đạn ngược lại nếu nhân vật quay trái
            bullet.transform.localScale = new Vector3(-1, 1, 1);
            bullet.GetComponent<Bullet>().speed *= -1;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu nhân vật chạm đất
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
