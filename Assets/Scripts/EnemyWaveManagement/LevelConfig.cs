using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Spawning/LevelConfig")]
public class LevelConfig : ScriptableObject {
    public List<WaveSegment> waves;
}