using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerComponentManager : MonoBehaviour
{
    public List<GameObject> activeComponentControllers = new List<GameObject>();

    // Context
    public EnemySpawnManager enemySpawnManager;
    public GameObject player;

    // 
    //
    // temp to make ram do something
    // 
    public int ramProjectileBonus = 0;
    // 
    // 

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
        //
        // temp to make ram do something
        // 
        if (weaponController.GetComponent<WeaponComponentInstance>().UIData.name == "Ram")
        {
            // add projectile to all weapons
            //  
            ramProjectileBonus += 1;
        // 
        // 
        }
        else
        {
            weaponController.transform.position = transform.position;
            weaponController.transform.rotation = Quaternion.identity;
            weaponController.transform.parent = transform;

            activeComponentControllers.Add(weaponController);
        }


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
