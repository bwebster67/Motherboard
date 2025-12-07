using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ComponentSelectionUIManager : MonoBehaviour
{
    public GameObject sideWindowGrid;
    public ComponentFactory componentFactory;
    public GameObject selectionComponentUIPrefab;

    void Awake()
    {
        if (componentFactory == null) componentFactory = FindAnyObjectByType<ComponentFactory>();
    }

    public void PopulateMenuWithComponents()
    {
        List<GameObject> choices = componentFactory.GetComponentChoices();
        foreach (GameObject choice in choices)
        {
            if (choice != null)
            {
                AddComponent(choice);
            }
        }
    }

    public void AddComponent(GameObject component)
    {
        GameObject selectionComponentUIGO = Instantiate(original: selectionComponentUIPrefab, parent: sideWindowGrid.transform);
        selectionComponentUIGO.GetComponent<SelectionComponent>().AssignComponent(component);
    }

    public void ClearMenu()
    {
        foreach (Transform child in sideWindowGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
