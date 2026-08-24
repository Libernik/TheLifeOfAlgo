using System.Linq;

public class HashRuleHard : IHashRule
{
    public int Calculate(string input)
    {
        int vowels = input.Count(c =>
            "аеёиоуыэюя".Contains(char.ToLower(c)));

        return input.Length * vowels;
    }

    public string GetHint()
    {
        return "Код = длина имени × количество гласных HARD";
    }
}