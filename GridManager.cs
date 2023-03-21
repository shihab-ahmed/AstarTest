using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridSizeX;
    public int gridSizeY;
    public Node[,] grid;
    ///public float nodeRadius;
    //float nodeDiameter;
    //public Vector2 gridWorldSize;
    public float pointDistance;
    //public void CreateGrid()
    //{
    //    nodeDiameter = nodeRadius * 2;
    //    gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
    //    gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
    //    grid = new Node[gridSizeX, gridSizeY];


    //    Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        
    //    for (int x = 0; x < gridSizeX; x++)
    //    {
    //        for (int y = 0; y < gridSizeY; y++)
    //        {
    //            Vector3 worldPoint = worldBottomLeft + Vector3.right * ( x * nodeDiameter + nodeRadius) + Vector3.forward * ( y * nodeDiameter + nodeRadius);
    //            grid[x, y] = new Node(worldPoint,x,y);                
    //        }
    //    }
    //}
    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * pointDistance) + Vector3.forward * (y * pointDistance);
                grid[x, y] = new Node(worldPoint, x, y);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (grid!=null)
        {
            foreach (Node node in grid)
            {
                if(node.isPathNode)
                {
                    Gizmos.color = Color.cyan;
                    //Gizmos.DrawLine(node.wordPosition, node.parentNode.wordPosition);
                }
                else
                {
                    switch (node.nodeState)
                    {
                        case NodeState.NORMAL:
                            Gizmos.color = Color.white;
                            break;
                        case NodeState.OPEN:
                            Gizmos.color = Color.green;
                            break;
                        case NodeState.CLOSED:
                            Gizmos.color = Color.red;
                            break;
                        case NodeState.BLOCK:
                            Gizmos.color = Color.black;
                            break;
                    }
                }
                
                
                Gizmos.DrawSphere(node.wordPosition, .5f);
            }
           
        }
    }

    public List<Node> GetNeighbour(Node node)
    {
        List<Node> neighbours = new List<Node>();

        if (node.gridX > 0)
        {
            neighbours.Add(grid[node.gridX - 1, node.gridY]);
        }
        if (node.gridX < gridSizeX - 1)
        {
            neighbours.Add(grid[node.gridX + 1, node.gridY]);
        }
        if (node.gridY > 0)
        {
            neighbours.Add(grid[node.gridX, node.gridY - 1]);
        }
        if (node.gridY < gridSizeY - 1)
        {
            neighbours.Add(grid[node.gridX, node.gridY + 1]);
        }
        if (node.gridX < gridSizeX - 1 && node.gridY < gridSizeY - 1)
        {
            neighbours.Add(grid[node.gridX + 1, node.gridY + 1]);
        }
        if (node.gridX < gridSizeX - 1 && node.gridY > 0)
        {
            neighbours.Add(grid[node.gridX + 1, node.gridY - 1]);
        }
        if (node.gridX > 0 && node.gridY < gridSizeY - 1)
        {
            neighbours.Add(grid[node.gridX - 1, node.gridY + 1]);
        }
        if (node.gridX > 0 && node.gridY > 0)
        {
            neighbours.Add(grid[node.gridX - 1, node.gridY - 1]);
        }
        // Debug.Log(neighbours.Count);
        return neighbours;

    }
}
