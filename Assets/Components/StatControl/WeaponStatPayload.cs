
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeaponStatPayload : ScriptableObject {
    
    public Dictionary<WeaponStat, float> weaponStatDict = new Dictionary<WeaponStat, float>();

    public void AddStat(WeaponStat stat, float value)
    {
        // i have no wifi so im not sure what conditions tryadd returns off of
        // 
        //  plan is, if stat isnt in dict, add it. If stat is in dict, add the value to the existing entry
        weaponStatDict.TryAdd(stat, value);
    }

}