using UnityEngine;
using System.Collections.Generic;

public class PlayerComponentManager : MonoBehaviour
{
    public List<GameObject> activeComponentControllerPrefabs = new List<GameObject>();

    void Start()
    {
    }

    public void AddWeapon(GameObject weaponController)
    {
        GameObject weaponLogicGO = Instantiate(
            weaponController,
            transform.position,
            Quaternion.identity,
            this.transform
        );

        activeComponentControllerPrefabs.Add(weaponController);
    }
    
}
