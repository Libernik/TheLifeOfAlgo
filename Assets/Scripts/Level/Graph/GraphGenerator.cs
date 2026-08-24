using UnityEngine;

public class GraphGenerator : ILevelGenerator<GraphLevelData>
{
    public GraphLevelData Generate(LevelConfig config)
    {
        GraphLevelData data =
            new GraphLevelData();
        int count = config.itemCount;
        data.graph = new GraphData();

        data.graph.nodeCount = count;

        data.graph.adjacencyMatrix =
            new int[count, count];

        // ---------- CONNECT GRAPH ----------
        for (int i = 1; i < count; i++)
        {
            int connectTo =
                Random.Range(0, i);

            int state =
                Random.Range(1, 3);

            data.graph
                .adjacencyMatrix[i, connectTo]
                    = state;

            data.graph
                .adjacencyMatrix[connectTo, i]
                    = state;
        }

        // ---------- EXTRA EDGES ----------
        for (int y = 0; y < count; y++)
        {
            for (int x = y + 1; x < count; x++)
            {
                if (data.graph
                    .adjacencyMatrix[x, y] != 0)
                    continue;

                bool add =
                    Random.value > 0.6f;

                if (!add)
                    continue;

                int state =
                    Random.Range(1, 3);

                data.graph
                    .adjacencyMatrix[x, y]
                        = state;

                data.graph
                    .adjacencyMatrix[y, x]
                        = state;
            }
        }

        return data;
    }
}