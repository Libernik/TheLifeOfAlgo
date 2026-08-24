using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatrixCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueText;

    [SerializeField] private Image background;

    private int value = 0;

    public int Value => value;

    void Start()
    {
        Refresh();
    }

    public void OnClick()
    {
        value++;

        if (value > 2)
            value = 0;

        Refresh();
    }

    void Refresh()
    {
        valueText.text = value.ToString();

        switch (value)
        {
            case 0:
                background.color =
                    Color.gray;
                break;

            case 1:
                background.color =
                    Color.red;
                break;

            case 2:
                background.color =
                    Color.green;
                break;
        }
    }

    public void SetValue(int v)
    {
        value = v;

        Refresh();
    }
}