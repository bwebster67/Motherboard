using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class GridDragManager : MonoBehaviour
{
    // Dragging
    private ComponentPointer draggedComponentPointer;
    private Vector2Int dragOffset;
    private Vector2Int? currentHoveredSlotCoords;
    private Vector2Int? draggedComponentFormerAnchor;

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
        ComponentPointer componentPointer = motherboardGrid.grid[gridCoords.x, gridCoords.y];
        
        if (componentPointer == null)
        {
            Debug.Log($"No component to drag in slot ({gridCoords.x}, {gridCoords.y})");
            return;
        }

        // instead of this, call to GridUIManager to disable the UI, unless drag fails
        // motherboardGrid.RemoveComponentEverywhere(componentGO, componentInstance.anchorPosition);
        // draggedComponentFormerAnchor = componentInstance.anchorPosition;
        dragOffset = gridCoords - componentPointer.gridAnchorPosition;
        draggedComponentPointer = componentPointer;
    }

    void StopDrag()
    {
        // If nothing is being dragged, stop
        if (draggedComponentPointer == null) return;

        // If dropping outside of a valid slot, reset component and stop
        if (currentHoveredSlotCoords == null)
        {
            Debug.Log("Component dropped out of grid.");
            draggedComponentPointer = null;
            dragOffset = new Vector2Int(0, 0);
            return;
        }

        // If being placed on a valid slot, try to place
        Vector2Int targetAnchor = currentHoveredSlotCoords.Value - dragOffset;
        Debug.Log($"targetAnchor: ({targetAnchor.x}, {targetAnchor.y}) = currentHoveredSlotCoords ({currentHoveredSlotCoords.Value.x}, {currentHoveredSlotCoords.Value.y}) - dragOffset ({dragOffset.x}, {dragOffset.y})");
        // If can place in this slot 
        if (motherboardGrid.CanPlaceComponent(draggedComponentPointer.componentController, targetAnchor))
        {
            // place component and remove it from the old spot
            motherboardGrid.MoveComponent(draggedComponentPointer, targetAnchor);
            Debug.Log($"Component successfully moved to ({targetAnchor.x}, {targetAnchor.y})");
            // Remove component from old spot.
            // motherboardGrid.RemoveComponentEverywhere(draggedComponent, draggedComponentFormerAnchor.Value);
        }
        draggedComponentPointer = null;
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
        StopDrag();
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
        Vector2Int nullLocation = new Vector2Int(-1, -1);
        ComponentPointer componentPointer = ScriptableObject.CreateInstance<ComponentPointer>();
        componentPointer.Init(componentGO, nullLocation, nullLocation);
        draggedComponentPointer = componentPointer;
    }

    void HandleSelectionSlotPointerUp(GameObject componentGO)
    {
        Debug.Log("SelectionSlot Mouse Up");
        if (draggedComponentPointer == null) return;
        Vector2Int targetAnchor = currentHoveredSlotCoords.Value;
        if (motherboardGrid.PlaceComponentEverywhere(draggedComponentPointer.componentController, targetAnchor))
        {
            Debug.Log($"Component successfully placed at ({targetAnchor.x}, {targetAnchor.y})");
            // componentSelectionUIManager.ClearMenu();
        }
        draggedComponentPointer = null;
        dragOffset = new Vector2Int(0, 0);
    }

}