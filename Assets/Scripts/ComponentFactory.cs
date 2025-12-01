using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;

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

    public List<GameObject> GetComponentChoices()
    {
        if (componentPrefabs.Count < 3)
        {
            Debug.LogError("Must have 3 or more components in ComponentFactory to choose from.");
            return null;
        } 
        List<GameObject> tempComponentPrefabs = new List<GameObject>(componentPrefabs);
        List<GameObject> choices = new List<GameObject>();
        
        for (int i = 0; i < 3; i++)
        {
            int index = UnityEngine.Random.Range(0, tempComponentPrefabs.Count - 1); 
            choices.Add(tempComponentPrefabs[index]);
            tempComponentPrefabs.RemoveAt(index);
        }

        return choices;
    }
}
