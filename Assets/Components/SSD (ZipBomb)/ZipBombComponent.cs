using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ZipBombComponent : WeaponComponentInstance
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
        Debug.Log("ZipBomb Attack!!!");
        StartCoroutine(nameof(ZipBombAttackSequence));
    }

    IEnumerator ZipBombAttackSequence()
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
            velocityVector = (target.transform.position - playerTransform.position).normalized * instanceSpeed;

            GameObject zipBombGO = projectilePool.GetProjectile();
            ZipBombProjectile zipBombProjScript = zipBombGO.GetComponent<ZipBombProjectile>();
            zipBombProjScript.velocityVector = velocityVector;

            yield return new WaitForSeconds(0.25f);
        }
    }

}
