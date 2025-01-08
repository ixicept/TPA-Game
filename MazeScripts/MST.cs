using System.Collections.Generic;
using UnityEngine;

public class MST : MonoBehaviour
{
    public List<Edge> GenerateMST(List<Node> nodes)
    {
        List<Edge> edges = new List<Edge>();
        List<Edge> mst = new List<Edge>();

        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = i + 1; j < nodes.Count; j++)
            {
                float weight = Vector3.Distance(nodes[i].position, nodes[j].position);
                edges.Add(new Edge(nodes[i], nodes[j], weight));
            }
        }
        edges.Sort();

        UnionFind uf = new UnionFind(nodes.Count);
        foreach (Edge edge in edges)
        {
            int root1 = uf.Find(edge.node1.id);
            int root2 = uf.Find(edge.node2.id);

            if (root1 != root2)
            {
                mst.Add(edge);
                uf.Union(root1, root2);
            }
        }

        return mst;
    }
}
