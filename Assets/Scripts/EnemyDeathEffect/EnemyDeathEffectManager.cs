using Unity.VisualScripting;
using UnityEngine;

class EnemyDeathEffectManager : MonoBehaviour 
{
    public static GenericObjectPool PoolInstance { get; set; }
    public GameObject enemyHitEffectPrefab;
    void Awake()
    {
        PoolInstance = gameObject.AddComponent<GenericObjectPool>();
        PoolInstance.Init(enemyHitEffectPrefab);
    }
    public static void SpawnEnemyDeathEffect(Vector3 position)
    {
        GameObject deathEffectGO = PoolInstance.GetObject();
        deathEffectGO.transform.position = position;
        ParticleSystem particleSystem = deathEffectGO.GetComponent<ParticleSystem>();
        particleSystem.Play();
    }
}