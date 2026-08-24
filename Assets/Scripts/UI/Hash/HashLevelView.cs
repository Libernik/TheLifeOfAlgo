using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HashLevelView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private PersonItem personPrefab;
    [SerializeField] private PackageItem packagePrefab;

    [Header("Parents")]
    [SerializeField] private Transform leftPanel;
    [SerializeField] private Transform rightPanel;

    [Header("UI")]
    [SerializeField] private TMPro.TextMeshProUGUI hintText;

    private List<PersonItem> persons = new();
    private List<PackageItem> packages = new();

    public List<PersonItem> Persons => persons;
    public List<PackageItem> Packages => packages;

    public void Build(HashLevelData data)
    {
        Clear();

        hintText.text = data.hint;

        // люди
        for (int i = 0; i < data.names.Length; i++)
        {
            var person = Instantiate(personPrefab, rightPanel);

            person.Init(data.names[i]);

            persons.Add(person);
        }

        // перемешиваем коды
        var shuffled = data.codes.OrderBy(x => Random.value).ToArray();

        // посылки
        for (int i = 0; i < shuffled.Length; i++)
        {
            var package = Instantiate(packagePrefab, leftPanel);

            package.Init(shuffled[i]);

            packages.Add(package);
        }
    }

    private void Clear()
    {
        foreach (var p in persons)
            Destroy(p.gameObject);

        foreach (var p in packages)
            Destroy(p.gameObject);

        persons.Clear();
        packages.Clear();
    }
}