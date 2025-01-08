using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject mazeCellPrefab;
    public int width;
    public int height;

    private MazeCell[,] mazeGrid; 

    void GenerateMaze()
    {
        mazeGrid = new MazeCell[width, height];

        // Create maze cells
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, 0, y);
                GameObject cellObject = Instantiate(mazeCellPrefab, cellPosition, Quaternion.identity);
                MazeCell mazeCell = cellObject.GetComponent<MazeCell>();
                mazeCell.Initialize(cellPosition);
                mazeGrid[x, y] = mazeCell;
            }
        }

    }

}
