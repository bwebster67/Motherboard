using Unity.VisualScripting;
using UnityEngine;

public abstract class GenericPooledObject : MonoBehaviour
{
    // Reference to the pool so the bullet can return itself
    private GenericObjectPool _pool; 

    public void Init(GenericObjectPool pool)
    {
        _pool = pool;
    }
    protected virtual void Update()
    {
    }

    protected virtual void OnEnable()
    {
        Reset();
    }

    protected virtual void Deactivate()
    {
        // Cancel invoke to prevent errors if deactivated early
        StopAllCoroutines();
        CancelInvoke(nameof(Deactivate)); 

        
        // Return to pool
        _pool.ReturnObject(gameObject);
    }

    protected virtual void Reset()
    {
        transform.position = transform.position;
        transform.rotation = Quaternion.identity;
    }

}