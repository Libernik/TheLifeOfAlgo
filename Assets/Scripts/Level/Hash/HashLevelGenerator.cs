using UnityEngine;

public class HashLevelGenerator : ILevelGenerator<HashLevelData>
{
    string[] syllables = { "ал", "эл", "ар", "эр", "тор", "гар", "вил", "бер", "мор", "лан", "ти", "рик", "гард", "рагн" };

    // Новый массив титулов
    string[] titles = {
        "Лорд", "Сэр", "Барон", "Граф", "Маркиз",
        "Рыцарь", "Магистр", ""
    };

    public HashLevelData Generate(LevelConfig config)
    {
        HashLevelData data = new HashLevelData();
        int count = config.itemCount;
        data.names = new string[count];
        data.codes = new int[count];
        IHashRule rule = HashRuleFactory.Create(config.hashRuleType);
        for (int i = 0; i < count; i++)
        {
            string name = GenerateName();

            data.names[i] = name;
            data.codes[i] = rule.Calculate(name);
        }

        data.hint = rule.GetHint();

        return data;
    }

    string GenerateName()
    {
        int count = Random.Range(2, 4);

        string name = "";

        for (int i = 0; i < count; i++)
        {
            name += syllables[Random.Range(0, syllables.Length)];
        }

        name = char.ToUpper(name[0]) + name.Substring(1);

        string title = titles[Random.Range(0, titles.Length)];

        return title + " " + name;
    }
}
