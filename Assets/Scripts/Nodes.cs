using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Nodes 
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Nodes connectedTo;

    public Nodes(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
