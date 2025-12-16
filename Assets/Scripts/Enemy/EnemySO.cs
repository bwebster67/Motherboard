using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public GameObject prefab; // visuals
    public float baseMoveSpeed = 2;
    public float baseHealth = 10;
    public float baseCollisionDamage = 1;
    public float baseAttackDamage = 1;
    public float expValue = 1;
}
