using System.Collections;
using TMPro;
using UnityEngine;

public class ZipBombProjectile : PlayerProjectile 
{
    public Vector3 velocityVector = new Vector3(0, 0, 0);
    private float elapsedTime = 0;
    public float fuseTime = 2;
    public float explosionRadius = 3;
    public bool hitEnemy = false;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Deactivate()
    {
        base.Deactivate();
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        base.OnTriggerEnter2D(collider2D);
    }

    protected override void OnEnemyHit(Collider2D collider2D)
    {
        Debug.Log("ZipBomb hit enemy!");
        hitEnemy = true;
    }

    protected override void Reset()
    {
        elapsedTime = 0;
        hitEnemy = false;
        base.Reset();
    }

    IEnumerator Kaboom()
    {
        onHitEffect.Play();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.NameToLayer("Enemy")); 
        foreach (Collider2D hitCollider in hitColliders)
        {
            hitCollider.TryGetComponent(out Enemy enemyScript);
            if (enemyScript != null) {
                enemyScript.Stun(0.25f);
                ApplyKnockback(enemyScript);
                DamageEnemy(enemyScript);
            }                
        }
        myCollider.enabled = false;
        myRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Deactivate();
    }

    protected override void Update()
    {
        if (enabled)
        {
            base.Update();
            elapsedTime += Time.deltaTime;
            if (!hitEnemy)
            {
                transform.position = transform.position + velocityVector * Time.deltaTime;
            }
            if (elapsedTime > fuseTime)
            {
                StartCoroutine(nameof(Kaboom));
                elapsedTime = -10;
            }
        }
    }

}
