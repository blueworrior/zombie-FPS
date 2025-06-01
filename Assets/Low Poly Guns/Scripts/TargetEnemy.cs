using Unity.VisualScripting;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [SerializeField] private HealthBarScript healthBar;

    private void Start()
    {
        currentHealth = maxHealth;

        // initialzing health bar
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }
    }

  public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
