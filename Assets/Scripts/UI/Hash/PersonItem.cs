using UnityEngine;
using TMPro;

public class PersonItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    public string personName;

    public Transform startParent { get; private set; }

    void Awake()
    {
        startParent = transform.parent;
    }

    public void Init(string name)
    {
        personName = name;
        nameText.text = name;
    }

    public void ResetPosition()
    {
        transform.SetParent(startParent);
        transform.localPosition = Vector3.zero;
    }
}