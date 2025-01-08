using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    public Node startNode;
    public Node targetNode;

    public List<Node> FindPath(Node start, Node target)
    {
        List<Node> path = new List<Node>();

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost ||
                    (openSet[i].fCost == currentNode.fCost &&
                     openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == target)
            {
                path = RetracePath(start, target);
                break;
            }

            foreach (Node neighbor in currentNode.neighbors)
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                int newCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, target);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return path;
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs((int)nodeA.position.x - (int)nodeB.position.x);
        int dstZ = Mathf.Abs((int)nodeA.position.z - (int)nodeB.position.z);

        return dstX + dstZ;
    }
}
