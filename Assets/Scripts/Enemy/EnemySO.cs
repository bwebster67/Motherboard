using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public GameObject prefab; // visuals
    public float baseMoveSpeed;
    public float baseHealth;
    public float baseCollisionDamage;
    public float baseAttackDamage;
    public float expValue;
}
