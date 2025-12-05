using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ByteBlastComponent : WeaponComponentInstance
{

    override protected void Start()
    {
        base.Start();  
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
        StartCoroutine(nameof(ByteBlastAttackSequence));
    }

    IEnumerator ByteBlastAttackSequence()
    {
        Transform target;
        Vector3 velocityVector;
        for (int i = 0; i < weaponData.projectileCount; i++)
        {
            // Assign closest enemy as target
            target = FindNearestEnemy(playerComponentManager.enemySpawnManager.activeEnemies);
            if (target == null) 
            {
                break; 
            }
            velocityVector = (target.transform.position - playerComponentManager.player.transform.position).normalized * weaponData.speed;

            for (int j = 0; j < 8; j++)
            {
                // 
                // OBJECT POOLING PLS???
                // instantiating 8 things in a row lags
                // 
                GameObject bit = projectilePool.GetProjectile();
                StartCoroutine(bit.GetComponent<ByteBlastProjectile>().Blast(velocityVector));
                
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
