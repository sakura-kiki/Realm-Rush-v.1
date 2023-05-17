using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Nodes currentSearchNode;

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    
    //Local variable
    Dictionary<Vector2Int, Nodes> grid;


    void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            //Transferring the public grid to the local one
            //Without breaking the public grid, we can manipulate the local one
            grid = gridManager.Grid;
        }
    }
    void Start()
    {
        ExploreNeighbors();
    }

    void ExploreNeighbors()
    {
        List<Nodes> neighbors = new List<Nodes>();

        Debug.Log(neighbors);

        Debug.Log(directions);
        foreach(Vector2Int direction in directions)
        {
            Debug.Log(direction);
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
            Debug.Log(neighborCoords);

            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);

                //TODO: Remove after testing
                grid[neighborCoords].isExplored = true;
                grid[currentSearchNode.coordinates].isPath = true;
            }
        }
    }
}
