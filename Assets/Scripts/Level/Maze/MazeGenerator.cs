using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : ILevelGenerator<MazeData>
{
    private Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    private MazePathfinder pathfinder = new();

    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public MazeData Generate(LevelConfig config)
    {
        int width = config.width;
        int height = config.height;

        MazeData maze = new MazeData(width, height);

        Stack<Vector2Int> stack = new();

        Vector2Int current = new Vector2Int(0, 0);

        maze.cells[0, 0].visited = true;

        stack.Push(current);

        while (stack.Count > 0)
        {
            current = stack.Pop();

            List<Vector2Int> neighbors = GetUnvisitedNeighbors(maze, current);

            if (neighbors.Count > 0)
            {
                stack.Push(current);

                Vector2Int next = neighbors[
                    Random.Range(0, neighbors.Count)];

                RemoveWall(maze, current, next);

                maze.cells[next.x, next.y].visited = true;

                stack.Push(next);
            }
        }

        GetShortestPath(maze, width, height);

        ConvertMazeToTask(maze);

        maze.hardTask.Image = config.taskImage;

        SetCorrectRune(maze);

        return maze;
    }

    List<Vector2Int> GetUnvisitedNeighbors(MazeData maze, Vector2Int pos)
    {
        List<Vector2Int> result = new();

        foreach (Vector2Int dir in directions)
        {
            Vector2Int next = pos + dir;

            if (next.x < 0 || next.y < 0 ||
                next.x >= maze.width ||
                next.y >= maze.height)
                continue;

            if (!maze.cells[next.x, next.y].visited)
            {
                result.Add(next);
            }
        }

        return result;
    }

    void RemoveWall(MazeData maze, Vector2Int a, Vector2Int b)
    {
        Vector2Int delta = b - a;

        if (delta == Vector2Int.up)
        {
            maze.cells[a.x, a.y].wallTop = false;
            maze.cells[b.x, b.y].wallBottom = false;
        }
        else if (delta == Vector2Int.down)
        {
            maze.cells[a.x, a.y].wallBottom = false;
            maze.cells[b.x, b.y].wallTop = false;
        }
        else if (delta == Vector2Int.left)
        {
            maze.cells[a.x, a.y].wallLeft = false;
            maze.cells[b.x, b.y].wallRight = false;
        }
        else if (delta == Vector2Int.right)
        {
            maze.cells[a.x, a.y].wallRight = false;
            maze.cells[b.x, b.y].wallLeft = false;
        }
    }

    void GetShortestPath(MazeData maze, int width, int height)
    {
        List<Vector2Int> solutionPath = pathfinder.FindPath(maze, new Vector2Int(0, 0), new Vector2Int(width - 1, height - 1));

        maze.path = new MazePathResult
        {
            pathLength = solutionPath.Count,
            path = solutionPath
        };
    }

    public char GetRandomRune()
    {
        return alphabet[Random.Range(0, alphabet.Length)];
    }

    void ConvertMazeToTask(MazeData maze)
    {
        System.Text.StringBuilder sb = new();

        int vertexCount = maze.width * maze.height;

        sb.AppendLine(vertexCount.ToString());

        for (int y = 0; y < maze.height; y++)
        {
            for (int x = 0; x < maze.width; x++)
            {
                int current = y * maze.width + x;

                MazeCell cell = maze.cells[x, y];

                if (!cell.wallRight && x < maze.width - 1)
                {
                    int right = y * maze.width + (x + 1);
                    sb.AppendLine($"{current} {right}");
                }

                if (!cell.wallTop && y < maze.height - 1)
                {
                    int top = (y + 1) * maze.width + x;
                    sb.AppendLine($"{current} {top}");
                }
            }
        }

        maze.hardTask = new HardTask
        {
            Description = "Перед тобой лабиринт в виде графа (список рёбер). Нажми на него, чтобы скопировать данные. Сначала найди длину кратчайшего пути между началом и выходом и введи это число." +
            " После этого лабиринт откроется, при входе начнётся отсчёт времени. Внутри на каждой клетке будут руны. Чтобы понять верную, найди программой кратчайший путь," +
            " вычисли его длину в клетках и сложи цифры этого числа.Результат — порядковый номер буквы в алфавите. Ориентируйся по руне с этой буквой," +
            " чтобы выйти.",

            InputData = sb.ToString(),

            ExpectedAnswer = maze.path.pathLength.ToString()
        };

        Debug.Log($"ответ = {maze.path.pathLength.ToString()}");
    }


    void SetCorrectRune(MazeData maze)
    {
        string answer = maze.hardTask.ExpectedAnswer;

        int num = 0;
        foreach (char i in answer)
        {
            num += i - '0';
        }

        maze.correctRune = alphabet[num - 1];

        Debug.Log($"руна = {maze.correctRune}");
    }
}