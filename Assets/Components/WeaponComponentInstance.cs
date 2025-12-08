using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class WeaponComponentInstance : ComponentInstance
{
    // Stats
    public WeaponData weaponData;
    protected float currentCooldown;
    protected float currentCooldownDuration;
    protected float instanceDamage;
    protected float instanceSpeed;
    protected int instancePierce;
    protected int instanceProjectileCount;
    
    // Pooling 
    public ProjectilePool projectilePool;


    public void OnEnable()
    {
        // TEMP FOR RAM TO WORK
        instanceProjectileCount = weaponData.projectileCount + playerComponentManager.ramProjectileBonus;
    }    


    protected override void Start()
    {
        base.Start();

        currentCooldownDuration = weaponData.cooldownDuration; 
        currentCooldown = currentCooldownDuration; 
        instanceDamage = weaponData.damage;
        instanceSpeed = weaponData.speed;
        instancePierce = weaponData.pierce;
        instanceProjectileCount = weaponData.projectileCount + playerComponentManager.ramProjectileBonus;

        projectilePool = gameObject.AddComponent<ProjectilePool>();
        projectilePool.Init(weaponData.projectilePrefab, weaponData);
    }

    protected override void Update()
    {
        base.Update();
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f) 
        {
            Attack();
        }
    }

    virtual public void Attack()
    {
        currentCooldown = weaponData.cooldownDuration;
    }

    public Transform FindNearestEnemy(List<Transform> enemies)
    {
        // uses squared distance for optimization
        // stack overflow says it avoids the expensive sqrt operation
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        return bestTarget;
    }
}
