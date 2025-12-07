using UnityEngine;
using System.Collections.Generic;

public class PlayerComponentManager : MonoBehaviour
{
    public List<GameObject> activeComponentControllers = new List<GameObject>();

    // Context
    public EnemySpawnManager enemySpawnManager;
    public GameObject player;

    void Awake()
    {
        if (player == null) { player = GameObject.FindGameObjectWithTag("Player"); }
    }

    void Start()
    {
    }

    public void DisableAllComponents()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public bool AddWeapon(GameObject weaponController)
    {
        // 
        // must take in an INSTANCE of the GO, not just the prefab. I think.. 
        // 
        weaponController.transform.position = transform.position;
        weaponController.transform.rotation = Quaternion.identity;
        weaponController.transform.parent = transform;

        activeComponentControllers.Add(weaponController);
        return true;
    }
    
    public bool RemoveWeapon(GameObject weaponController)
    {
        int idx = activeComponentControllers.IndexOf(weaponController);
        activeComponentControllers.Remove(weaponController);
        Destroy(transform.GetChild(idx).gameObject);
        return true;
    }
}
