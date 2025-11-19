using UnityEngine;
using System.Collections.Generic;

public class ComponentUICreator : MonoBehaviour
{
    public List<ComponentUIData> componentsData;

    public ComponentUIData GetComponent(int index)
    {
        if ((index >= 0) && (index < componentsData.Count))
        {
            return componentsData[index];
        }
        Debug.LogError($"GetComponent index {index} out of range");
        return null;
    }
}
