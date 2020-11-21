using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform[] _chunks;
    [SerializeField] private Vector3 _cellSize = new Vector3(0, 0, 0);

    private float _firstPointX = -3;
    private float _firstPointY = -25;
    private List<MazeGeneratorCell[,]> _listMazes = new List<MazeGeneratorCell[,]>();

    private void Awake()
    {
        for (int i = 0; i < _chunks.Length; i++)
        {
            MazeGenerator mazeGenerator = new MazeGenerator(7, 32);
            MazeGeneratorCell[,] maze = mazeGenerator.Maze;
            _listMazes.Add(maze);
        }

    }

    private void Start()
    {
        for (int c = 0; c < _chunks.Length; c++)
        {
            Transform chunk = _chunks[c];
            MazeGeneratorCell[,] maze = _listMazes[c];
            float y = _firstPointY;

            for (int j = 0; j < maze.GetLength(1); j++)
            {
                float x = _firstPointX;

                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    GameObject spawned = Instantiate(_cellPrefab, chunk);
                    Cell cell = spawned.GetComponent<Cell>();
                    cell.WallLeft.SetActive(maze[i, j].WallLeft);
                    cell.WallBottom.SetActive(maze[i, j].WallBottom);
                    spawned.transform.position = new Vector3(x * _cellSize.x, y * _cellSize.y, y * _cellSize.z);
                    x += 1;
                }

                y += 1;
            }

            _firstPointY += -(maze.GetLength(1) - 1);
        }
    }
}
