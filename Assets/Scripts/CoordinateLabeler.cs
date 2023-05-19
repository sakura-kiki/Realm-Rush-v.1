using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
//Move the script to a new folder called "Editor" later
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]

public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.black;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);


    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
    }

    void Update()
    {

        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjetName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void SetLabelColor()
    {
        if (gridManager == null) { return; }

        Nodes node = gridManager.GetNode(coordinates);

        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }
        //since we are working in x,z coordinates in 3D world, but they are named as x,y coordinates in 2D world
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnitGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnitGridSize);

        label.text = $"{coordinates.x},{coordinates.y}";

    }
    void UpdateObjetName()
    {
        transform.parent.name = coordinates.ToString();
    }
}



