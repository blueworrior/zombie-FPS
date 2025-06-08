using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead = false;

    [SerializeField] private playerHealthBar playerHealthBar;

    public Text healthCouter;
    private void Awake()
    {
        singleton = this;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        playerHealthBar.UpdateHealthBar(maxHealth, currentHealth);
        UpdateHealthCounter();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerDamage(float damage)
    {
        if (currentHealth > 0)
        {
            if (damage >= currentHealth)
            {
                Dead();

            }
            else
            {
                currentHealth -= damage;
                playerHealthBar.UpdateHealthBar(maxHealth, currentHealth);

            }
            UpdateHealthCounter();
            
        }
    }

    void Dead()
    {
        currentHealth = 0;
        isDead = true;

        
        playerHealthBar.UpdateHealthBar(maxHealth, currentHealth);
        Debug.Log("Player is Dead");
        UpdateHealthCounter();

    }


    void UpdateHealthCounter()
    {
        healthCouter.text = currentHealth.ToString();
    }
}
