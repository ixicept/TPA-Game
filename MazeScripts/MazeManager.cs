using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public Transform startNode;
    public Transform targetNode;
    public Astar astar;

    void Start()
    {
        Node startNodeComponent = startNode.GetComponent<Node>();
        Node targetNodeComponent = targetNode.GetComponent<Node>();

        List<Node> path = astar.FindPath(startNodeComponent, targetNodeComponent);

        foreach (Node node in path)
        {
            Debug.Log("Next node in path: " + node.position);
        }
    }

}
