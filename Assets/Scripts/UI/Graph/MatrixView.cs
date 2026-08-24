using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MatrixView : MonoBehaviour
{
    [SerializeField] private MatrixCell cellPrefab;

    [SerializeField] private TextMeshProUGUI labelPrefab;

    [SerializeField] private Transform gridParent;

    private MatrixCell[,] cells;

    public void Build(int size)
    {
        Clear();

        cells = new MatrixCell[size, size];

        GridLayoutGroup grid = gridParent.GetComponent<GridLayoutGroup>();

        grid.constraint =
            GridLayoutGroup.Constraint
            .FixedColumnCount;

        grid.constraintCount = size + 1;

        CreateTopLabels(size);

        for (int y = 0; y < size; y++)
        {
            CreateSideLabel(y);

            for (int x = 0; x < size; x++)
            {
                MatrixCell cell =
                    Instantiate(
                        cellPrefab,
                        gridParent);

                cells[x, y] = cell;
            }
        }
    }

    void CreateTopLabels(int size)
    {
        Instantiate(labelPrefab, gridParent)
            .text = "";

        for (int i = 0; i < size; i++)
        {
            Instantiate(labelPrefab, gridParent)
                .text =
                ((char)('A' + i)).ToString();
        }
    }

    void CreateSideLabel(int row)
    {
        Instantiate(labelPrefab, gridParent)
            .text =
            ((char)('A' + row)).ToString();
    }

    void Clear()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        cells = null;
    }

    public int GetValue(int x, int y)
    {
        return cells[x, y].Value;
    }
}