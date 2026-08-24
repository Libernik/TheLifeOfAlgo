using System.Collections.Generic;
using UnityEngine;

public class GraphView : MonoBehaviour
{
    [SerializeField] private NodeView nodePrefab;
    [SerializeField] private EdgeView edgePrefab;

    [SerializeField] private RectTransform graphArea;

    private List<NodeView> nodes = new();

    public void Build(GraphData graph)
    {
        Clear();

        CreateNodes(graph);

        CreateEdges(graph);
    }

    void CreateNodes(GraphData graph)
    {
        float radius = 250f;

        for (int i = 0;
            i < graph.nodeCount;
            i++)
        {
            float angle =
                i * Mathf.PI * 2f
                / graph.nodeCount;

            Vector2 pos =
                new Vector2(
                    Mathf.Cos(angle),
                    Mathf.Sin(angle))
                * radius;

            NodeView node =
                Instantiate(
                    nodePrefab,
                    graphArea);

            node.Init(
                ((char)('A' + i)).ToString());

            RectTransform rect =
                node.GetComponent<RectTransform>();

            rect.anchoredPosition = pos;

            nodes.Add(node);
        }
    }

    void CreateEdges(GraphData graph)
    {
        for (int y = 0;
            y < graph.nodeCount;
            y++)
        {
            for (int x = y + 1;
                x < graph.nodeCount;
                x++)
            {
                int state = graph.adjacencyMatrix[x, y];

                if (state == 0) continue;

                EdgeView edge =
                    Instantiate(
                        edgePrefab,
                        graphArea);

                edge.transform.SetAsFirstSibling();

                edge.Connect(
                    nodes[x]
                    .GetComponent<RectTransform>(),

                    nodes[y]
                    .GetComponent<RectTransform>(),

                    state);
            }
        }
    }

    void Clear()
    {
        foreach (Transform child in graphArea)
            Destroy(child.gameObject);

        nodes.Clear();
    }
}