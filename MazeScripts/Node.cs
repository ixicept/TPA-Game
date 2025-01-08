using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int id;
    public Vector3 position;
    public Edge[] edges;
    public bool visited = false;
    public bool walkable = true;
    public int gCost;
    public int hCost;
    public Node parent;

    public List<Node> neighbors = new List<Node>();
    public int fCost { get { return gCost + hCost; } }
    public void AddNeighbor(Node neighbor, float weight)
    {
        Edge edge = new Edge(this, neighbor, weight);
        edges[edges.Length] = edge;
    }
}
