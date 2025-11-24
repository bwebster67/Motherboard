using UnityEngine; 

public abstract class Enemy : MonoBehaviour
{
    public EnemySO enemyData;
    public Rigidbody2D rigidbody2D;
    public GameObject player;
    // public IEnemyMoveBehavior enemyMoveBehavior;

    protected virtual void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        if (rigidbody2D == null) rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
    }

    protected virtual void Die()
    {
    }
}
