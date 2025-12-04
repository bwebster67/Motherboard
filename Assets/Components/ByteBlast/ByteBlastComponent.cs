using UnityEngine;

public class ByteBlastComponent : WeaponComponentInstance
{
    override protected void Start()
    {
        // Will be stored in ScriptableObject
        // weaponData.cooldownDuration = 3f;

    }
    
    override protected void Update()
    {
        base.Update();
    }
    
    override public void Attack()
    {
        base.Attack();
        // Instantiate(weaponData.prefab);
        Debug.Log("ByteBlast Attack!!! 01010101 01010101");
    }
}
