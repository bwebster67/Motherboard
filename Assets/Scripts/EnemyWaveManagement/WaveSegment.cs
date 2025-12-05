using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WaveSegment {
    public string segmentName; // For your own sanity
    public float duration;     // How long this wave lasts
    public float spawnRate;    // Time between spawns (0.1 = fast, 2.0 = slow)
    
    // Using string keys is better for Pooling than GameObjects references
    public string[] enemyTags; 
    
}
