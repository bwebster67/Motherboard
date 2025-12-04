using UnityEngine;
using System.Collections.Generic;

public class PlayerComponentManager : MonoBehaviour
{
    public List<GameObject> activeComponentControllerPrefabs = new List<GameObject>();

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

    public void AddWeapon(GameObject weaponController)
    {
        // 
        // must take in an INSTANCE of the GO, not just the prefab. I think.. 
        // 
        // GameObject weaponLogicGO = Instantiate(
        //     weaponController,
        //     transform.position,
        //     Quaternion.identity,
        //     this.transform
        // );
        weaponController.transform.position = transform.position;
        weaponController.transform.rotation = Quaternion.identity;
        weaponController.transform.parent = transform;

        activeComponentControllerPrefabs.Add(weaponController);
    }
    
}
