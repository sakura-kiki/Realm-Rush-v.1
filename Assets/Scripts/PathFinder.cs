using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;
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
        }
        startNode = new Nodes(startCoordinates, true);
        destinationNode = new Nodes(destinationCoordinates, true);
    }

    void Start()
    {
        BreadthFirstSearch();
    }

    void ExploreNeighbors()
    {
        List<Nodes> neighbors = new List<Nodes>();

        Debug.Log(neighbors);

        Debug.Log(directions);
        foreach (Vector2Int direction in directions)
        {
            Debug.Log(direction);
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
            Debug.Log(neighborCoords);

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }

            foreach (Nodes neighbor in neighbors)
            {
                if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
                {
                    reached.Add(neighbor.coordinates, neighbor);
                    frontier.Enqueue(neighbor);
                }
            }
        }

    }
    void BreadthFirstSearch()
    {
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
}
