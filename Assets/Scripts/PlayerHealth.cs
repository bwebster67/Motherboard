using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float currentHealth;
    public System.Action OnPlayerDied;
    public System.Action<float> OnPlayerTakeDamage;

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.tag == "Enemy")
    //     {
    //         OnPlayerDied.Invoke();
    //     }
    // }
    public float TakeDamage(float damage)
    {
        currentHealth -= damage; 

        OnPlayerTakeDamage.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            OnPlayerDied.Invoke();
        }

        return currentHealth;
    }

    void Start()
    {
        maxHealth = 10;
    }

}
