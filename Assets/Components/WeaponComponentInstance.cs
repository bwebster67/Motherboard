using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class WeaponComponentInstance : ComponentInstance
{
    public WeaponData weaponData;
    protected float currentCooldown;
    public ProjectilePool projectilePool;

    protected override void Start()
    {
        base.Start();
        currentCooldown = weaponData.cooldownDuration; 
        projectilePool = gameObject.AddComponent<ProjectilePool>();
        projectilePool.Init(weaponData.projectilePrefab, weaponData);
    }

    protected override void Update()
    {
        base.Update();
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)   //Once the cooldown becomes 0, attack
        {
            Attack();
        }
    }

    virtual public void Attack()
    {
        currentCooldown = weaponData.cooldownDuration;
    }

    public void SetData(WeaponData data)
    {
        this.weaponData = data;
        this.currentCooldown = data.cooldownDuration;
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
            Debug.Log($"{enemies.Count}");
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
