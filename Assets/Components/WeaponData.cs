using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Stats")]
    public GameObject projectilePrefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    public int pierce = 0;
    public int projectileCount = 1;
}