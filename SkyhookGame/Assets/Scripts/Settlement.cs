using System.Collections.Generic;

public class Settlement
{
    public List<Resource> resources;
    
    public List<Institution> institutions;
    
    public string Name { get; private set; }

    public int level;
    public int exp;

    public Settlement(List<Resource> resources, List<Institution> institutions, string name)
    {
        this.resources = resources;
        this.institutions = institutions;

        Name = name;

        level = 1;
        exp = 0;
    }

    private void AddXp(int amountToAdd)
    {
        exp += amountToAdd;

        // TODO: check for level increase
    }
}
