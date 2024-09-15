using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Waypoint : MonoBehaviour
{
    // For creating tower
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    BuildManager buildManager;
    public bool IsPlaceable { get { return isPlaceable; } }

    void Start()
    {
        buildManager = BuildManager.instance;
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
