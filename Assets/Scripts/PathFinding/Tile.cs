using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    // For creating tower
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;

    BuildManager buildManager;
    GridManager gridManager;
    Vector2Int coordinates;

    public bool IsPlaceable { get { return isPlaceable; } }

    void Start()
    {
        gridManager = GridManager.instance;
        HandleTile();

        buildManager = BuildManager.instance;
    }

    void HandleTile()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            //if (!gridManager.Grid[coordinates].isWalkable)
            if (gameObject.transform.parent.tag != "Path")
                gridManager.BlockNode(coordinates);
        }
    }

    void OnMouseDown()
    {
        if(isPlaceable)
        {
            bool isPlaced = buildManager.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
