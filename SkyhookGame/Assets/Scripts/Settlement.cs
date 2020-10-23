using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    [SerializeField] private ExperienceModule experienceModule = default;
    
    private List<Resource> resources;
    
    private List<Institution> institutions;
    
    public string Name { get; private set; }

    public Settlement(List<Resource> resources, List<Institution> institutions, string name)
    {
        this.resources = resources;
        this.institutions = institutions;

        Name = name;
    }

    public void AddExperience(int amount)
    {
        experienceModule.Increase(amount);

        Debug.Log(experienceModule.Experience);
    }
}
