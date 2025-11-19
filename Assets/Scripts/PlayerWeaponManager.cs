using UnityEngine;
using System.Collections.Generic;

public class PlayerWeaponManager : MonoBehaviour
{
    public List<WeaponComponentInstance> activeWeapons = new List<WeaponComponentInstance>();

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

        activeWeapons.Add(weaponController.GetComponent<WeaponComponentInstance>());
    }
    
}
