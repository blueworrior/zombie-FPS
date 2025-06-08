using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private AIMovement aiMovement; // Reference to AIMovement

    [SerializeField] private HealthBarScript healthBar;

    // Reference to the animator
    private Animator animator;

    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        currentHealth = maxHealth;

        // initialize health bar
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }

        // get animator component
        animator = GetComponent<Animator>();
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
         if (aiMovement != null)
            {
                aiMovement.DisableEnemy();
            }
        
        // Trigger the death animation
        animator.SetTrigger("die");

        // Destroy the zombie after 4 secs
        Destroy(gameObject);
    }
}