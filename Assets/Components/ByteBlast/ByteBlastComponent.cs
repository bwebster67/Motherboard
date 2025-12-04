using System.Collections;
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
        Debug.Log("ByteBlast Attack!!!");
        StartCoroutine("ByteBlastAttackSequence");
    }

    IEnumerator ByteBlastAttackSequence()
    {
        for (int i = 0; i < weaponData.projectileCount; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // 
                // OBJECT POOLING PLS???
                // 
                Instantiate(weaponData.prefab, transform.position, Quaternion.identity);
                float rand = Random.value;
                if (rand < 0.5) Debug.Log("0");
                else Debug.Log("1");
                
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    // void SpawnObject()
    // {
    //     Instantiate(weaponData.prefab, transform.position, Quaternion.identity);
    // }
}
