using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    // For creating tower
    [SerializeField] bool isPlaceable;

    BuildManager buildManager;
    GridManager gridManager;
    Vector2Int coordinates;

    public bool IsPlaceable { get { return isPlaceable; } set { isPlaceable = value; } }

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
            if (gameObject.transform.parent.tag != "Path")
                gridManager.BlockNode(coordinates);
        }
    }

    public Vector3 GetTilePostition()
    {
        return transform.position;
    }

    void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if (isPlaceable)
                buildManager.selectTile(this);
        }
    }
}
