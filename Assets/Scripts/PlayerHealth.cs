using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public System.Action OnPlayerDied;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            OnPlayerDied.Invoke();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
