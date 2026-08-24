using System.Collections.Generic;

public class PortalLevelData
{
    public List<PortalNodeData> nodes = new();

    public List<PortalEdgeData> edges = new();

    public int startNode;

    public int finishNode;

    public PortalPathResult shortestPath;

    public void Init()
    {

    }

    public void FindShortestPath()
    {
        int nodeCount = nodes.Count;

        int[] distance = new int[nodeCount];
        bool[] visited = new bool[nodeCount];
        int[] previous = new int[nodeCount];

        for (int i = 0; i < nodeCount; i++)
        {
            distance[i] = int.MaxValue;
            previous[i] = -1;
        }

        distance[startNode] = 0;

        while (true)
        {
            int current = -1;
            int bestDistance = int.MaxValue;

            // ищем ближайшую непосещённую вершину
            for (int i = 0; i < nodeCount; i++)
            {
                if (visited[i])
                    continue;

                if (distance[i] < bestDistance)
                {
                    bestDistance = distance[i];
                    current = i;
                }
            }

            // достижимых вершин больше нет
            if (current == -1)
                break;

            visited[current] = true;

            // финиш найден
            if (current == finishNode)
                break;

            foreach (var edge in edges)
            {
                int next;

                // граф НЕориентированный
                if (edge.from == current)
                {
                    next = edge.to;
                }
                else if (edge.to == current)
                {
                    next = edge.from;
                }
                else
                {
                    continue;
                }

                if (visited[next])
                    continue;

                int newDistance = distance[current] + edge.cost;

                if (newDistance < distance[next])
                {
                    distance[next] = newDistance;
                    previous[next] = current;
                }
            }
        }

        // восстановление пути
        List<int> path = new();

        if (distance[finishNode] != int.MaxValue)
        {
            int pathNode = finishNode;

            while (pathNode != -1)
            {
                path.Add(pathNode);
                pathNode = previous[pathNode];
            }

            path.Reverse();
        }

        shortestPath = new PortalPathResult
        {
            totalCost = distance[finishNode],
            islandsCount = path.Count,
            path = path
        };
    }


}