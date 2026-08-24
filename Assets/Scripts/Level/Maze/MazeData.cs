public class MazeData
{
    public int width;

    public int height;

    public MazeCell[,] cells;

    public MazePathResult path;

    public char correctRune;

    public HardTask hardTask;

    public MazeData(int w, int h)
    {
        width = w;
        height = h;

        cells = new MazeCell[w, h];

        for (int x = 0; x < w; x++) 
        {
            for (int y = 0; y < h; y++) 
            {
                cells[x, y] = new MazeCell();
            }
        }
    }
}