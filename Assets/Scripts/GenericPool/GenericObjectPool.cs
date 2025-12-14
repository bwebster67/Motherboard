using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class GenericObjectPool : MonoBehaviour
{
    public GameObject objectPrefab;
    private ObjectPool<GameObject> _pool;

    public void Init(GameObject obj)
    {
        this.objectPrefab = obj;
        // Initialize the pool
        _pool = new ObjectPool<GameObject>(
            createFunc: () => CreatePooledObject(obj), // How to create a new item if the pool is empty
            actionOnGet: (obj) => obj.SetActive(true),   // What to do when an item is taken from the pool
            actionOnRelease: (obj) => obj.SetActive(false), // What to do when an item is returned to the pool
            actionOnDestroy: (obj) => Destroy(obj),      // What to do if we destroy the pool itself
            collectionCheck: false, // Performance optimization (turn on for debugging only)
            defaultCapacity: 60,    // Initial size
            maxSize: 100            // Max size (prevents memory leaks if you spawn too many)
        );
    }

    GameObject CreatePooledObject(GameObject obj)
    {
        obj.SetActive(false);
        GameObject prefab = Instantiate(obj);
        prefab.GetComponent<GenericPooledObject>().Init(this);
        return prefab;
    }

    public GameObject GetObject()
    {
        return _pool.Get(); // Returns an active object from the pool
    }

    public void ReturnObject(GameObject obj)
    {
        _pool.Release(obj); // Disables it and puts it back in the pool
    }

}