using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 3f; // Tốc độ di chuyển của enemy
    public Transform groundCheck; // Vị trí để kiểm tra mặt đất (tạo một object nhỏ ở chân enemy)
    public LayerMask groundLayer; // Layer của nền đất
    public float groundCheckDistance = 1f; // Khoảng cách kiểm tra nền đất
    private bool movingRight = true; // Biến xác định hướng di chuyển

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Di chuyển enemy về hướng hiện tại
        rb.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.velocity.y);

        // Kiểm tra xem có chạm vào nền đất (tường hoặc mép sàn không)
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // Nếu không có nền đất, đổi hướng
        if (groundInfo.collider == null)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        movingRight = !movingRight;

        // Lật hình nhân vật để đi ngược lại
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
