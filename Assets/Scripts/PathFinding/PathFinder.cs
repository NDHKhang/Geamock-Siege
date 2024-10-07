using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    // For storing and tracking path
    Queue<Node> frontier = new Queue<Node>();
    // Store explored node
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    // Search 4 direction clockwise
    Vector2Int[] directionsClockWise = { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };
    Vector2Int[] directionsCounterClockWise = { Vector2Int.left, Vector2Int.down, Vector2Int.right, Vector2Int.up };

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {

        gridManager = GridManager.instance;
        if (gridManager != null )
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }

    public List<Node> GetPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void ExploreNeighbors(Vector2Int[] directions)
    {
        List<Node> neighbors = new List<Node>(); // Store neighbor explore node

        foreach (Vector2Int direction in directions)
        {
            // Explore each direction clockwise
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach(Node neighbor in neighbors)
        {
            // Check if neighbor node is walkable and avoid revisiting
            if(neighbor.isWalkable && !reached.ContainsKey(neighbor.coordinates))
            {
                // create connection
                neighbor.connectedTo = currentSearchNode;
                // mark explored node
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        frontier.Clear();
        reached.Clear();

        // Check flag
        bool isRunning = true;

        // Add node to be explored
        frontier.Enqueue(grid[coordinates]);
        // Track all the node have been explored, avoid revisiting
        reached.Add(coordinates, grid[coordinates]);

        Vector2Int[] directions;

        //Random path
        int randomNum = Random.Range(0, 2);
        if (randomNum == 0)
            directions = directionsClockWise;
        else
            directions = directionsCounterClockWise;

        while (frontier.Count > 0 && isRunning)
        {
            // get node from queue then mark the current node is explored
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;

            ExploreNeighbors(directions);

            // Check if current node reach destination or not
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        // Searching path backward
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        // Building Path
        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo; 
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }
}
