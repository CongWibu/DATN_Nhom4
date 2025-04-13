using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3; // Sức khỏe của enemy
    public float damage = 1f; // Sát thương của enemy
    public GameObject enemyBulletPrefab; // Prefab đạn của enemy
    public Transform firePoint; // Vị trí bắn đạn
    public float fireRate = 2f; // Tốc độ bắn
    public float moveSpeed = 3f; // Tốc độ di chuyển của enemy

    private float nextFireTime = 0f;
    private bool isMovingRight = true; // Hướng di chuyển của enemy (True = phải, False = trái)

    void Update()
    {
        // Kiểm tra thời gian để bắn đạn
        if (Time.time > nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + 1f / fireRate; // Cập nhật thời gian bắn tiếp theo
        }

        // Di chuyển enemy và lật hướng khi gặp biên
        Move();
    }

    void Fire()
    {
        // Tạo đạn từ firePoint
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);

        // Lật đạn theo hướng của enemy
        if (!isMovingRight)
        {
            bullet.transform.localScale = new Vector3(-1, 1, 1); // Quay đạn khi enemy di chuyển trái
            bullet.GetComponent<EnemyBullet>().speed *= -1; // Đổi hướng di chuyển của đạn
        }
    }

    void Move()
    {
        // Di chuyển enemy theo hướng
        float direction = isMovingRight ? 1f : -1f;
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // Thay đổi hướng di chuyển khi chạm biên
        if (transform.position.x > 5f) // Giới hạn bên phải
        {
            isMovingRight = false;
        }
        else if (transform.position.x < -5f) // Giới hạn bên trái
        {
            isMovingRight = true;
        }
    }

    public void TakeDamage(float damage) // Thêm tham số sát thương vào phương thức
    {
        health -= Mathf.FloorToInt(damage); // Giảm máu của enemy theo sát thương
        if (health <= 0)
        {
            Destroy(gameObject); // Enemy bị hủy khi hết máu
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Kiểm tra khi trúng đạn
        if (collider.CompareTag("Bullet"))
        {
            Bullet bullet = collider.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage); // Gây sát thương cho enemy
                Destroy(bullet.gameObject); // Hủy đạn sau khi trúng enemy
            }
        }

        // Kiểm tra khi va chạm với player
        if (collider.CompareTag("Player"))
        {
            // Gây sát thương cho player
            collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            // Giảm sức khỏe của enemy sau khi va chạm với player
            TakeDamage(damage);
        }
    }
}
