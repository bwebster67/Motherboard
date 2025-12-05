using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    public GameObject projectilePrefab;
    public WeaponData weaponData;
    
    private ObjectPool<GameObject> _pool;

    public void Init(GameObject projectilePrefab, WeaponData weaponData)
    {
        this.projectilePrefab = projectilePrefab;
        this.weaponData = weaponData;
        // Initialize the pool
        _pool = new ObjectPool<GameObject>(
            createFunc: () => CreatePooledProjectile(projectilePrefab), // How to create a new item if the pool is empty
            actionOnGet: (obj) => obj.SetActive(true),   // What to do when an item is taken from the pool
            actionOnRelease: (obj) => obj.SetActive(false), // What to do when an item is returned to the pool
            actionOnDestroy: (obj) => Destroy(obj),      // What to do if we destroy the pool itself
            collectionCheck: false, // Performance optimization (turn on for debugging only)
            defaultCapacity: 24,    // Initial size
            maxSize: 100            // Max size (prevents memory leaks if you spawn too many)
        );
    }

    GameObject CreatePooledProjectile(GameObject projectilePrefab)
    {
        projectilePrefab.SetActive(false);
        GameObject prefab = Instantiate(projectilePrefab);
        prefab.GetComponent<PlayerProjectile>().Init(this, weaponData);
        return prefab;
    }

    public GameObject GetProjectile()
    {
        return _pool.Get(); // Returns an active object from the pool
    }

    public void ReturnProjectile(GameObject bullet)
    {
        _pool.Release(bullet); // Disables it and puts it back in the pool
    }
}