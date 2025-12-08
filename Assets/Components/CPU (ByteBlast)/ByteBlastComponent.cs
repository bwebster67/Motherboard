using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ByteBlastComponent : WeaponComponentInstance
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
        Debug.Log("ByteBlast Attack!!!");
        StartCoroutine(nameof(ByteBlastAttackSequence));
    }

    IEnumerator ByteBlastAttackSequence()
    {
        Transform target;
        Vector3 velocityVector;
        for (int i = 0; i < instanceProjectileCount; i++)
        {
            // Assign closest enemy as target
            target = FindNearestEnemy(playerComponentManager.enemySpawnManager.activeEnemies);
            if (target == null) 
            {
                break; 
            }
            if (Vector2.Distance(target.position, transform.position) > 25)
            {
                break;
            }
            velocityVector = (target.transform.position - playerTransform.position).normalized * instanceSpeed;

            for (int j = 0; j < 8; j++)
            {
                GameObject bitGO = projectilePool.GetProjectile();
                ByteBlastProjectile bitScript = bitGO.GetComponent<ByteBlastProjectile>();
                bitScript.velocityVector = velocityVector;
                
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
