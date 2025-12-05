using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawnManager : MonoBehaviour
{
    // When you do object pooling
    //      maybe have an activeEnemies list and inactiveEnemies list 
    //      move between lists when activating and deactivating
    // 
    public List<Transform> activeEnemies;

    // Pooling 
    [System.Serializable]
    public struct PoolInfo {
        public string tag;
        public GameObject prefab;
        public int defaultSize;
        public int maxSize;
    }
    public List<PoolInfo> poolsToCreate;
    private Dictionary<string, ObjectPool<GameObject>> poolDictionary;

    void Awake()
    {
        InitializePools();
    }

    void Start()
    {
    }

    void InitializePools() {
        poolDictionary = new Dictionary<string, ObjectPool<GameObject>>();

        foreach (PoolInfo poolInfo in poolsToCreate) {

            GameObject prefab = poolInfo.prefab; 
            
            var newPool = new ObjectPool<GameObject>(
                createFunc: () => { return Instantiate(prefab, transform); },
                actionOnGet: (obj) => { obj.SetActive(true); },
                actionOnRelease: (obj) => { obj.SetActive(false); },
                actionOnDestroy: (obj) => { Destroy(obj); },
                collectionCheck: false,
                defaultCapacity: poolInfo.defaultSize,
                maxSize: poolInfo.maxSize
            );

            poolDictionary.Add(poolInfo.tag, newPool);
        }
    }


    public GameObject Spawn(string tag, Vector3 position, Quaternion rotation) {
        if (!poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning($"Pool tag '{tag}' not found!");
            return null;
        }

        GameObject obj = poolDictionary[tag].Get();
        activeEnemies.Add(obj.transform);
        
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        Enemy enemyScript = obj.GetComponent<Enemy>();
        enemyScript.SetPool(poolDictionary[tag]);

        return obj;
    }

}
