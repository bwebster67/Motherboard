using System.Data.Common;
using UnityEngine;

public class WeaponComponentInstance : ComponentInstance
{
    public WeaponData weaponData;
    protected float currentCooldown;

    protected override void Start()
    {
        currentCooldown = weaponData.cooldownDuration; 
    }

    protected override void Update()
    {
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
}
