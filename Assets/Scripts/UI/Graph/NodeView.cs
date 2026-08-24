using TMPro;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public void Init(string text)
    {
        label.text = text;
    }
}