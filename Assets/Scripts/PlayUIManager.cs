using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    public Slider expBarSlider;
    public Slider healthBarSlider;
    public PlayerLevelManager playerLevelManager;
    public PlayerHealth playerHealth;


    void OnEnable()
    {
        playerLevelManager.OnGainExp += HandleGainExp; 
        playerLevelManager.OnLevelUp += HandleLevelUp; 
        playerHealth.OnPlayerTakeDamage += HandlePlayerTakeDamage;
    }
    void OnDisable()
    {
        playerLevelManager.OnGainExp -= HandleGainExp; 
        playerLevelManager.OnLevelUp -= HandleLevelUp; 
        playerHealth.OnPlayerTakeDamage -= HandlePlayerTakeDamage;
    }

    void Awake()
    {
        if (playerLevelManager == null) playerLevelManager = FindAnyObjectByType<PlayerLevelManager>();
        if (playerHealth == null) playerHealth = FindAnyObjectByType<PlayerHealth>();
    }
    void Start()
    {
        UpdateExpThreshold(playerLevelManager.nextLevelThreshold);
        UpdateCurrentExp(playerLevelManager.playerExp);
        UpdateMaxHealth(playerHealth.maxHealth);
        UpdateCurrentHealth(playerHealth.currentHealth);
        Debug.Log($"maxHealth: {playerHealth.maxHealth}, currentHealth: {playerHealth.maxHealth}");
    }


    // Health Bar 
    private void HandlePlayerTakeDamage(float currentHealth)
    {
        UpdateCurrentHealth(currentHealth);
    }
    private void UpdateCurrentHealth(float currentHealth) {healthBarSlider.value = currentHealth;}
    private void UpdateMaxHealth(float maxHealth) {healthBarSlider.maxValue = maxHealth;}


    // Exp Bar
    private void HandleGainExp(float currentExp)
    {
        UpdateCurrentExp(currentExp);
    }
    private void HandleLevelUp(float nextLevelThreshold)
    {
        UpdateExpThreshold(nextLevelThreshold);
    }
    private void UpdateCurrentExp(float currentExp) {expBarSlider.value = currentExp;}
    private void UpdateExpThreshold(float nextLevelThreshold) {expBarSlider.maxValue = nextLevelThreshold;}
}
