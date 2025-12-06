using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class GridDragManager : MonoBehaviour
{
    // Dragging
    private GameObject draggedComponent;
    private Vector2Int dragOffset;
    private Vector2Int? currentHoveredSlotCoords;

    // Context
    MotherboardGrid motherboardGrid;

    public void Awake()
    {
        if (motherboardGrid == null) motherboardGrid = Object.FindAnyObjectByType<MotherboardGrid>();
    }

    public void OnEnable()
    {
        GridSlot.onGridSlotPointerDown += HandleGridSlotPointerDown;
        GridSlot.onGridSlotPointerUp += HandleGridSlotPointerUp;
        GridSlot.onGridSlotPointerEnter += HandleGridSlotPointerEnter;
        GridSlot.onGridSlotPointerExit += HandleGridSlotPointerExit;
        SelectionComponent.onSelectionSlotPointerDown += HandleSelectionSlotPointerDown;
        SelectionComponent.onSelectionSlotPointerUp += HandleSelectionSlotPointerUp;
    }

    public void OnDisable()
    {
        GridSlot.onGridSlotPointerDown -= HandleGridSlotPointerDown;
        GridSlot.onGridSlotPointerUp -= HandleGridSlotPointerUp;
        GridSlot.onGridSlotPointerEnter -= HandleGridSlotPointerEnter;
        GridSlot.onGridSlotPointerExit -= HandleGridSlotPointerExit;
        SelectionComponent.onSelectionSlotPointerDown -= HandleSelectionSlotPointerDown;
        SelectionComponent.onSelectionSlotPointerUp -= HandleSelectionSlotPointerUp;
    }

    void StartDrag(Vector2Int gridCoords)
    {
        GameObject componentGO = motherboardGrid.grid[gridCoords.x, gridCoords.y];
        ComponentInstance componentInstance = componentGO.GetComponent<ComponentInstance>();
        
        if (componentGO == null)
        {
            Debug.Log($"No component to drag in slot ({gridCoords.x}, {gridCoords.y})");
            return;
        }

        motherboardGrid.RemoveComponentEverywhere(componentGO, componentInstance.anchorPosition);
        dragOffset = gridCoords - componentInstance.anchorPosition;
        draggedComponent = componentGO;
    }

    void StopDrag(Vector2Int gridCoords)
    {
        if (draggedComponent == null) return;

        if (currentHoveredSlotCoords == null)
        {
            // Component was dropped outside of the grid, do not place
            Debug.Log("Component dropped out of grid.");
            motherboardGrid.AddComponentEverywhere(draggedComponent, gridCoords- dragOffset);
            draggedComponent = null;
            dragOffset = new Vector2Int(0, 0);
            return;
        }

        Vector2Int targetAnchor = currentHoveredSlotCoords.Value - dragOffset;
        Debug.Log($"targetAnchor: ({targetAnchor.x}, {targetAnchor.y}) = gridCoords ({gridCoords.x}, {gridCoords.y}) - dragOffset ({dragOffset.x}, {dragOffset.y})");
        if (motherboardGrid.AddComponentEverywhere(draggedComponent, targetAnchor))
        {
            Debug.Log($"Component successfully placed at ({targetAnchor.x}, {targetAnchor.y})");
        }
        else
        {
            motherboardGrid.AddComponentEverywhere(draggedComponent, gridCoords - dragOffset);
        }
        draggedComponent = null;
        dragOffset = new Vector2Int(0, 0);
    }

    // GridSlot Component Handling
    void HandleGridSlotPointerDown(Vector2Int gridCoords, PointerEventData pointerEventData)
    {
        Debug.Log($"Mouse down on slot ({gridCoords.x}, {gridCoords.y})");
        StartDrag(gridCoords);
    }
    void HandleGridSlotPointerUp(Vector2Int gridCoords, PointerEventData pointerEventData)
    {
        Debug.Log($"Mouse up on slot ({gridCoords.x}, {gridCoords.y})");
        StopDrag(gridCoords);
    }

    void HandleGridSlotPointerEnter(Vector2Int gridCoords, PointerEventData pointerEventData)
    {
        Debug.Log($"Hovering over ({gridCoords.x}, {gridCoords.y})");
        currentHoveredSlotCoords = gridCoords;
    }

    void HandleGridSlotPointerExit(Vector2Int gridCoords, PointerEventData pointerEventData)
    {
        currentHoveredSlotCoords = null;
    }

    // Selection Component Handling
    void HandleSelectionSlotPointerDown(GameObject componentGO)
    {
        Debug.Log("SelectionSlot Mouse Down");
        draggedComponent = componentGO;
    }

    void HandleSelectionSlotPointerUp(GameObject componentGO)
    {
        Debug.Log("SelectionSlot Mouse Up");
        if (draggedComponent == null) return;
        Vector2Int targetAnchor = currentHoveredSlotCoords.Value;
        if (motherboardGrid.AddComponentEverywhere(draggedComponent, targetAnchor))
        {
            Debug.Log($"Component successfully placed at ({targetAnchor.x}, {targetAnchor.y})");
            // componentSelectionUIManager.ClearMenu();
        }
        draggedComponent = null;
        dragOffset = new Vector2Int(0, 0);
    }

}