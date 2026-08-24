using UnityEngine;
using UnityEngine.UI;

public class EdgeView : MonoBehaviour
{
    [SerializeField] private RectTransform line;

    [SerializeField] private Image image;

    public void Connect(
        RectTransform a,
        RectTransform b,
        int state)
    {
        Vector3 dir = b.position - a.position;

        line.position =
            (a.position + b.position) / 2f;

        line.sizeDelta =
            new Vector2(dir.magnitude, 5f);

        float angle =
            Mathf.Atan2(dir.y, dir.x)
            * Mathf.Rad2Deg;

        line.rotation =
            Quaternion.Euler(0, 0, angle);

        switch (state)
        {
            case 0:
                image.color =
                    Color.gray;
                break;

            case 1:
                image.color =
                    Color.red;
                break;

            case 2:
                image.color =
                    Color.green;
                break;
        }
    }
}