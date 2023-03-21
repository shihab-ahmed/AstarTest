using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    GridManager gridManager;
    public Node startNode;
    public Node targetNode;
    public List<Node> openSet;
    public List<Node> Path;
    private void Awake()
    {
        gridManager = GetComponent<GridManager>();
    }
    private void Start()
    {
        gridManager.CreateGrid();
        FindPath();
    }
    void FindPath()
    {
        startNode = gridManager.grid[0, 0];
        //targetNode = gridManager.grid[gridManager.gridSizeX-1, gridManager.gridSizeY - 1];
        targetNode = gridManager.grid[15, 20];
        openSet = new List<Node>();
        startNode.nodeState = NodeState.OPEN;
        openSet.Add(startNode);

        while (openSet.Count>0)
        {
            Node currentNode = openSet[0];
            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].fCost<currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost<currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            currentNode.nodeState = NodeState.CLOSED;
            openSet.Remove(currentNode);

            if (currentNode == targetNode)
            {
                Debug.Log("PathFound");
                RetracePath(startNode,targetNode);
                return;
            }

            foreach (var neighbour in gridManager.GetNeighbour(currentNode))
            {
                if (neighbour.nodeState == NodeState.BLOCK || neighbour.nodeState == NodeState.CLOSED) continue;

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                if(newMovementCostToNeighbour < neighbour.gCost || neighbour.nodeState != NodeState.OPEN)
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parentNode = currentNode;

                    if(neighbour.nodeState != NodeState.OPEN)
                    {
                        neighbour.nodeState = NodeState.OPEN;
                        openSet.Add(neighbour);
                    }
                }
            }
        }

    }
    void RetracePath(Node sNode, Node eNode)
    {
        Node currentNode = eNode;
        while (currentNode != sNode)
        {
            Path.Add(currentNode);
            currentNode.isPathNode = true;
            currentNode = currentNode.parentNode;
        }
        Path.Reverse();

    }
    private int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.gridX - b.gridX);
        int dstY = Mathf.Abs(a.gridY - b.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}
