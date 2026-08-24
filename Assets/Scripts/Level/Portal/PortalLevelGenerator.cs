using UnityEngine;
using System.Collections.Generic;

public class PortalLevelGenerator : ILevelGenerator<PortalLevelData>
{
    private int multiplier = 5;

    public PortalLevelData Generate(LevelConfig config)
    {
        PortalLevelData data = new();

        GenerateNodes(data);

        GenerateEdges(data);

        return data;
    }

    void GenerateNodes(PortalLevelData data)
    {
        float spacingX = 6f;
        float spacingY = 3.5f;

        for (int i = 0; i < multiplier; i++)
        {
            for (int j = 0; j < multiplier; j++)
            {
                float jitterX = Random.Range(-0.1f, 0.1f);

                float jitterY = Random.Range(-0.1f, 0.1f);
                 
                Vector2 pos =
                    new Vector2( -13 + j * spacingX + jitterX, 7f - i * spacingY + jitterY);       

                data.nodes.Add(
                    new PortalNodeData
                    {
                        position = pos
                    });
            }
        }

        // START + FINISH рядом

        data.startNode = multiplier * (multiplier - 1);

        data.finishNode = multiplier - 1;
    }

    void GenerateEdges(       //TODO: назначать время игроку по формуле (длина пути по порталам) + (кол-во островов на пути * 1 секунда)
     PortalLevelData data)
    {
        int start = data.startNode;
        int finish = data.finishNode;

        //AddEdge(data, start, finish, 20, PortalType.Slow);

        for (int i = 0; i < multiplier; i++)
        {
            for (int j = 0; j < multiplier; j++)
            {
                int numOfIsland = multiplier * i + j;
                int numOfRightIsland = numOfIsland + 1;
                int numOfBottomIsland = numOfIsland + multiplier;

                if (numOfRightIsland != multiplier * (i + 1))
                {
                    AddRandomEdge(data, numOfIsland, numOfRightIsland);
                }

                if (numOfBottomIsland < multiplier * multiplier)
                {
                    AddRandomEdge(data, numOfIsland, numOfBottomIsland);
                }
            }
        }

    }

    void AddEdge(PortalLevelData data, int from, int to, int cost, PortalType type)
    {
        data.edges.Add(
            new PortalEdgeData
            {
                from = from,
                to = to,
                cost = cost,
                type = type
            });
    }

    void AddRandomEdge(PortalLevelData data, int from, int to)
    {
        int r = Random.Range(0, 10);

        if (r < 5)
        {
            AddEdge(
                data,
                from,
                to,
                2,
                PortalType.Fast);
        }
        else if (r < 9)
        {
            AddEdge(
                data,
                from,
                to,
                5,
                PortalType.Medium);
        }
        else
        {
            AddEdge(
                data,
                from,
                to,
                10,
                PortalType.Slow);
        }
    }

}