using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform[] _chunks;
    [SerializeField] private Vector3 _cellSize = new Vector3(0, 0, 0);
    [SerializeField] private Transform _cameraTransform;

    private float _firstPointX = -3;
    private float _firstPointZ = -25;
    private List<MazeGeneratorCell[,]> _listMazes = new List<MazeGeneratorCell[,]>();
    private Vector3 _checkPoint = new Vector3();
    private int _currentChunk = 0;

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

            SpawnCell(chunk, maze);
            SetNewCheckPoint(maze);
        }
    }

    private void SpawnCell(Transform chunk, MazeGeneratorCell[,] maze)
    {
        float z = _firstPointZ;
        for (int j = 0; j < maze.GetLength(1); j++)
        {
            float x = _firstPointX;
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                GameObject spawned = Instantiate(_cellPrefab, chunk);
                Cell cell = spawned.GetComponent<Cell>();

                SetCell(maze, cell, i, j, x, z);
                x += 1;
            }

            z += 1;
        }
    }

    private void Update()
    { 
        if (Vector3.Distance(_cameraTransform.position, _checkPoint) < 250)
            RespawnChunk();
    }

    private void RespawnChunk()
    {
        MazeGenerator mazeGenerator = new MazeGenerator(7, 32);
        MazeGeneratorCell[,] maze = mazeGenerator.Maze;

        List<Transform> childrens = _chunks[_currentChunk].GetComponentsInChildren<Transform>().ToList();
        List<Transform> cells3d = childrens.Where(wv => wv.GetComponent<Cell>() != false).ToList();

        float z = _firstPointZ;
        int current = 0;

        for (int j = 0; j < maze.GetLength(1); j++)
        {
            float x = _firstPointX;
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                Cell cell = cells3d[current].GetComponent<Cell>();
                SetCell(maze, cell, i, j, x, z);

                x += 1;
                current++;
            }

            z += 1;
        }

        SetNewCheckPoint(maze);

        _currentChunk++;
        if (!(_currentChunk < _chunks.Length))
            _currentChunk = 0;
    }

    private void SetNewCheckPoint(MazeGeneratorCell[,] maze)
    {
        _checkPoint = new Vector3(_cameraTransform.position.x, _cameraTransform.position.y, _firstPointZ * 10);
        _firstPointZ += -(maze.GetLength(1) - 1);
    }

    private void SetCell(MazeGeneratorCell[,] maze, Cell cell, int i, int j, float x, float z)
    {

        cell.WallLeft.SetActive(maze[i, j].WallLeft);
        cell.WallBottom.SetActive(maze[i, j].WallBottom);

        cell.transform.position = new Vector3(x * _cellSize.x, z * _cellSize.y, z * _cellSize.z);
    }
}
