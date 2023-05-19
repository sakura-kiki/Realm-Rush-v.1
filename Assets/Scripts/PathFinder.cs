using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }
    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestionationCoordinates { get { return destinationCoordinates; } }
    Nodes startNode;
    Nodes destinationNode;
    Nodes currentSearchNode;
    Queue<Nodes> frontier = new Queue<Nodes>();
    Dictionary<Vector2Int, Nodes> reached = new Dictionary<Vector2Int, Nodes>();
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;

    //*Local variable
    Dictionary<Vector2Int, Nodes> grid = new Dictionary<Vector2Int, Nodes>();


    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            //*Transferring the public grid to the local one
            //*Without breaking the public grid, we can manipulate the local one
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }

    void Start()
    {
        GetNewPath();
    }

    public List<Nodes> GetNewPath()
    {
        gridManager.ResetNode();
        BreadthFirstSearch();
        return BuildPath();
    }
    void ExploreNeighbors()
    {
        List<Nodes> neighbors = new List<Nodes>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }

            foreach (Nodes neighbor in neighbors)
            {
                if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
                {
                    neighbor.connectedTo = currentSearchNode;
                    reached.Add(neighbor.coordinates, neighbor);
                    frontier.Enqueue(neighbor);
                }
            }
        }

    }
    void BreadthFirstSearch()
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Nodes> BuildPath()
    {
        List<Nodes> path = new List<Nodes>();
        Nodes currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Nodes> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }
}
