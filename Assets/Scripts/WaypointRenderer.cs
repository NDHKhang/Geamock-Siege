using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (Waypoint))]
public class WaypointRenderer : MonoBehaviour
{
    // For better visual
    [SerializeField] Color hoverColor;
    Color startColor;
    Renderer rend;
    Waypoint waypoint;

    void Awake()
    {
        rend = transform.Find("Mesh").GetComponent<Renderer>();
        waypoint = GetComponent<Waypoint>();
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
        if (waypoint.IsPlaceable)
            rend.materials[1].color = hoverColor;
    }
    void OnMouseExit()
    {
        if (transform.parent.tag != "Path")
            rend.materials[1].color = startColor;
    }
}
