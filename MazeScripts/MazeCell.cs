using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public bool isWalkable = true; 
    public Vector3Int position; 
    public void Initialize(Vector3Int position)
    {
        this.position = position;
    }
}
