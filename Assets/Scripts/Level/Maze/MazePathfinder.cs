using UnityEngine;
using System.Collections.Generic;

public class MazePathfinder
{
    public List<Vector2Int> FindPath(MazeData maze, Vector2Int start, Vector2Int end)
    {
        Queue<Vector2Int> queue = new();

        Dictionary<Vector2Int, Vector2Int>
            parent = new();

        queue.Enqueue(start);

        parent[start] = start;

        Vector2Int[] dirs =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == end)
                break;

            foreach (Vector2Int dir in dirs)
            {
                Vector2Int next = current + dir;

                if (next.x < 0 || next.y < 0 || next.x >= maze.width || next.y >= maze.height) 
                    continue;

                if (parent.ContainsKey(next))
                    continue;

                if (!CanMove(maze, current, dir))
                    continue;

                queue.Enqueue(next);

                parent[next] = current;
            }
        }

        List<Vector2Int> path = new();

        Vector2Int node = end;

        while (node != start)
        {
            path.Add(node);
            node = parent[node];
        }

        path.Add(start);

        path.Reverse();

        return path;
    }

    bool CanMove(MazeData maze, Vector2Int pos, Vector2Int dir)
    {
        MazeCell cell =
            maze.cells[pos.x, pos.y];

        if (dir == Vector2Int.up)
            return !cell.wallTop;

        if (dir == Vector2Int.down)
            return !cell.wallBottom;

        if (dir == Vector2Int.left)
            return !cell.wallLeft;

        if (dir == Vector2Int.right)
            return !cell.wallRight;

        return false;
    }
}