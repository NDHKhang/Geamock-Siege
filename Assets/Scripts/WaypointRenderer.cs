using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (Tile))]
public class WaypointRenderer : MonoBehaviour
{
    // For better visual
    [SerializeField] Color hoverColor;
    Color startColor;
    Renderer rend;
    Tile tile;

    void Awake()
    {
        rend = transform.Find("Mesh").GetComponent<Renderer>();
        tile = GetComponent<Tile>();
    }

    void Start()
    {
        if (transform.parent.tag != "Path")
            MaterialColor();
    }

    void MaterialColor()
    {
        Material[] materials = rend.materials;
        startColor = materials[1].color;
    }

    void OnMouseEnter()
    {
        if (tile.IsPlaceable)
            rend.materials[1].color = hoverColor;
    }
    void OnMouseExit()
    {
        if (transform.parent.tag != "Path")
            rend.materials[1].color = startColor;
    }
}
