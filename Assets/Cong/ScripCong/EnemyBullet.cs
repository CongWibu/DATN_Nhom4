using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f; // Tốc độ di chuyển của đạn
    public int damage = 1; // Sát thương của đạn

    private void Update()
    {
        // Di chuyển đạn theo hướng sang phải (hoặc trái nếu cần)
        transform.Translate(Vector2.left * speed * Time.deltaTime); // Ví dụ đạn bay sang trái
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu đạn va vào player, gây sát thương cho player
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Gây sát thương cho player
            }
            Destroy(gameObject); // Hủy đạn sau khi va chạm
        }
    }
}