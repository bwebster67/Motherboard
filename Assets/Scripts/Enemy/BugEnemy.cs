using UnityEngine; 

public class BugEnemy : Enemy , IEnemyMoveBehavior
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Move(rigidbody2D, player.transform, enemyData.baseMoveSpeed);
    }

    public override float TakeDamage(float amount)
    {
        return base.TakeDamage(amount);
    }

    protected override void Die()
    {
        base.Die();
    }

    public void Move(Rigidbody2D rb, Transform target, float speed)
    {
        if (target != null)
        {
            Vector2 direction = (target.position - rb.transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed* Time.fixedDeltaTime);
        }  

        // rotates enemy to face player
        if (target.position.x < rb.transform.position.x) { rb.transform.rotation = new Quaternion(0, 180, 0, 0); }
        else if (target.position.x >= rb.transform.position.x) { rb.transform.rotation = new Quaternion(0, 0, 0, 0); }
    }

}
