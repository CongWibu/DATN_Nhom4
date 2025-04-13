using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5;

    public void TakeDamage(float damage)
    {
        health -= Mathf.FloorToInt(damage);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Làm gì khi player chết, ví dụ: reload lại scene
        Destroy(gameObject);
    }
}
