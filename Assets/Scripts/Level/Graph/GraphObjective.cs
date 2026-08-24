public class GraphObjective :
    ILevelObjective
{
    private GraphData graph;
    private MatrixView matrix;

    public GraphObjective(
        GraphData graph,
        MatrixView matrix)
    {
        this.graph = graph;
        this.matrix = matrix;
    }

    public bool Check()
    {
        for (int y = 0;
            y < graph.nodeCount;
            y++)
        {
            for (int x = 0;
                x < graph.nodeCount;
                x++)
            {
                int player =
                    matrix.GetValue(x, y);

                int correct =
                    graph.adjacencyMatrix[x, y];
                    

                if (player != correct)
                    return false;
            }
        }

        return true;
    }
}
