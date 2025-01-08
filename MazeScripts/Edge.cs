using UnityEngine;

public class Edge : System.IComparable<Edge>
{
    public Node node1;
    public Node node2;
    public float weight;

    public Edge(Node node1, Node node2, float weight)
    {
        this.node1 = node1;
        this.node2 = node2;
        this.weight = weight;
    }

    public int CompareTo(Edge other)
    {
        return this.weight.CompareTo(other.weight);
    }
}
