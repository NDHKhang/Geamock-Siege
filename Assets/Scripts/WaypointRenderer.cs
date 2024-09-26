using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof (Tile))]
public class WaypointRenderer : MonoBehaviour
{
    // For better visual
    [SerializeField] Color hoverColor;
    Color startColor;
    Renderer rend;
    Tile tile;

    BuildManager buildManager;

    void Awake()
    {
        rend = transform.Find("Mesh").GetComponent<Renderer>();
        tile = GetComponent<Tile>();
    }

    void Start()
    {
        if (transform.parent.tag != "Path")
            MaterialColor();

        buildManager = BuildManager.instance;
    }

    void MaterialColor()
    {
        Material[] materials = rend.materials;
        startColor = materials[1].color;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (tile.CanPlaceTower)
            rend.materials[1].color = hoverColor;
    }

    void OnMouseExit()
    {
        if (tile.CanPlaceTower)
            rend.materials[1].color = startColor;
    }
}
