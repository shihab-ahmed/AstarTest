using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Node
{
    public NodeState nodeState = NodeState.NORMAL;
    public Node parentNode;
    public bool isPathNode;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public int drag;
    public int fCost
    {
        get { return gCost + hCost + drag; }
    }
    public Vector3 wordPosition;
    public Node(Vector3 pos, int gridX, int gridY)
    {
        wordPosition = pos;
        this.gridX = gridX;
        this.gridY = gridY;
    }
}