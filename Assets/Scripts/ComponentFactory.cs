using UnityEngine;
using System.Collections.Generic;

public class ComponentFactory : MonoBehaviour
{
    public List<GameObject> componentPrefabs;

    public GameObject GetComponent(int index)
    {
        if ((index >= 0) && (index < componentPrefabs.Count))
        {
            return componentPrefabs[index];
        }
        Debug.LogError($"GetComponent index {index} out of range");
        return null;
    }
}
