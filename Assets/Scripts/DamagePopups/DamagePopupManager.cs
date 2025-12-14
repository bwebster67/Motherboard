using Unity.VisualScripting;
using UnityEngine;

class DamagePopupManager : MonoBehaviour 
{
    public static GenericObjectPool PoolInstance { get; set; }
    public GameObject damagePopupPrefab;
    void Awake()
    {
        PoolInstance = gameObject.AddComponent<GenericObjectPool>();
        PoolInstance.Init(damagePopupPrefab);
    }
    public static void SpawnDamagePopup(Vector3 position, float value)
    {
        GameObject popup = PoolInstance.GetObject();
        popup.GetComponent<DamagePopup>().SetValue(value);
        // Jitter so it's not in the same spot every time
        Vector3 jitter = new Vector3 (((float)Random.Range(-10, 10)) / 30, ((float)Random.Range(-10, 10)) / 30, ((float)Random.Range(-10, 10)) / 30);
        popup.transform.position = position + jitter;
    }
}