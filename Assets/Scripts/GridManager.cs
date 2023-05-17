using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int,Nodes> grid = new Dictionary<Vector2Int, Nodes>();
    public Dictionary<Vector2Int,Nodes> Grid {get {return grid;}}

    void Awake() 
    {
        CreateGrid();
    }

    public Nodes GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        
        return null;
    }
    void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates,new Nodes(coordinates, true));
                Debug.Log(grid[coordinates].coordinates + "=" + grid[coordinates].isWalkable);
            }
        }
    }
}
