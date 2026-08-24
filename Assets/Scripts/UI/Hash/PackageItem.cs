using UnityEngine;
using TMPro;

public class PackageItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI codeText;
    public int code;

    public void Init(int value)
    {
        code = value;
        codeText.text = value.ToString();
    }
}
