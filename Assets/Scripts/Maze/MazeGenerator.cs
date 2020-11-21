using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool WallLeft { get; set; }
    public bool WallBottom { get; set; }
    public bool IsVisited { get; private set; }

    public MazeGeneratorCell(int x, int y)
    {
        X = x;
        Y = y;
        WallLeft = true;
        WallBottom = true;
        IsVisited = false;
    }

    public void Visit()
    {
        IsVisited = true;
    }
}

public class MazeGenerator
{
    public MazeGeneratorCell[,] Maze { get; private set; }

    private int _width;
    private int _height;

    public MazeGenerator(int width, int height)
    {
        _width = width;
        _height = height;

        Maze = new MazeGeneratorCell[width, height];

        GenerateMaze();
        RemoveWallsWithBacktracker(Maze);
    }

    private void GenerateMaze()
    {
        for (int x = 0; x < Maze.GetLength(0); x++)
            for (int y = 0; y < Maze.GetLength(1); y++)
                Maze[x, y] = new MazeGeneratorCell(x, y);

        for (int y = 0; y < Maze.GetLength(1); y++)
            Maze[_width - 1, y].WallBottom = false;

        for (int x = 0; x < Maze.GetLength(0); x++)
        {
            Maze[x, _height - 1].WallLeft = false;
            Maze[x, 0].WallBottom = false;
        }
    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell cell = maze[0, 0];
        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();

        cell.Visit();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbors = new List<MazeGeneratorCell>();

            if (cell.X > 0 && !maze[cell.X - 1, cell.Y].IsVisited)
                unvisitedNeighbors.Add(maze[cell.X - 1, cell.Y]);

            if (cell.Y > 0 && !maze[cell.X, cell.Y - 1].IsVisited)
                unvisitedNeighbors.Add(maze[cell.X, cell.Y - 1]);

            if (cell.X < _width - 2 && !maze[cell.X + 1, cell.Y].IsVisited)
                unvisitedNeighbors.Add(maze[cell.X + 1, cell.Y]);

            if (cell.Y < _height - 2 && !maze[cell.X, cell.Y + 1].IsVisited)
                unvisitedNeighbors.Add(maze[cell.X, cell.Y + 1]);

            if (unvisitedNeighbors.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
                RemoveWall(cell, chosen);

                chosen.Visit();
                cell = chosen;
                stack.Push(cell);
            }
            else
            {
                cell = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y)
                a.WallBottom = false;
            else
                b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X)
                a.WallLeft = false;
            else
                b.WallLeft = false;
        }
    }
}
