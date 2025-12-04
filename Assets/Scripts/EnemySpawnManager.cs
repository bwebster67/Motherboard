using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public List<Transform> enemies;
    public GameObject bugEnemyPrefab;

    void Start()
    {
        // implement wave system next, this is just for testing
        GameObject bug = Instantiate(bugEnemyPrefab);
        enemies.Add(bug.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
