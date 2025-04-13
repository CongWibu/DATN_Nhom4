using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public float damage = 1f; // Đổi kiểu thành float

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Tự hủy sau vài giây
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Gây sát thương cho enemy
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Truyền giá trị float
            }

            Destroy(gameObject); // Hủy đạn sau khi bắn
        }
    }
}
