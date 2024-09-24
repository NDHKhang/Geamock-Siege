using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable; // Check if node is walkable/moveable
    public bool isExplored; // Check if node is explored or not
    public bool isPath; // Flag for store path node
    public Node connectedTo; // Store the next node

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
