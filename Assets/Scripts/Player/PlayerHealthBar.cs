using UnityEngine;
using UnityEngine.UI;

public class playerHealthBar : MonoBehaviour
{
    // Reference to the UI Image that visually represents the health bar
    [SerializeField] private Image healthbarSprite;
    // Speed at which the health bar smoothly decreases to its new value
    [SerializeField] private float reduceSpeed = 2;
    // Target fill amount (between 0 and 1) that the bar will smoothly move toward
    private float target = 1;

    // Reference to the main camera to make the health bar face the player
    private Camera cam;

    void Start()
    {
        // Get the main camera when the game starts
        cam = Camera.main;
    }

    // Called by other scripts to update the target health value
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        // Calculate target fill amount based on current health
        target = currentHealth / maxHealth;
    }

  void Update()
  {
    // Rotate the health bar to always face the camera (billboarding effect)
    //transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);

    // Smoothly transition the fill amount toward the target value
    healthbarSprite.fillAmount = Mathf.MoveTowards(healthbarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    Debug.Log(healthbarSprite.fillAmount);

    }
}
