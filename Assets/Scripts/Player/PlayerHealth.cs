using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead = false;

    private void Awake()
    {
        singleton = this;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;

        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        currentHealth = 0;
        isDead = true;

        Debug.Log("Player is Dead");
        
    }
}
