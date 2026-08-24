using System.Collections.Generic;

public class HashMatchObjective : ILevelObjective
{
    private List<PackageItem> packages;
    private IHashRule rule;

    public HashMatchObjective(List<PackageItem> packages, IHashRule rule)
    {
        this.packages = packages;
        this.rule = rule;
    }

    public bool Check()
    {
        foreach (var package in packages)
        {
            var drop = package.GetComponent<DropZone>();

            if (drop.currentPerson == null)
                return false;

            int correct =
                rule.Calculate(
                    drop.currentPerson.personName);

            if (correct != package.code)
                return false;
        }

        return true;
    }
}