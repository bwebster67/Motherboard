using UnityEngine;

public class RamComponent : WeaponComponentInstance
{
    override protected void Start()
    {
        base.Start();
    }
    
    override protected void Update()
    {
        base.Update();
    }
    
    override public void Attack()
    {
        base.Attack();
        // Instantiate(weaponData.prefab);
        Debug.Log("Ram Placeholder Action");
    }
}
