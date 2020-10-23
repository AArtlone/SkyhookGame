public class HumanSector : Institution
{
    public int TotalHumans { get; private set; }
    public int AvailableHumans { get; private set; }

   /// <summary>
   /// Checks if there are enough available humans
   /// </summary>
    public bool CheckIfCanPerform(int requiredAmount) 
    { 
        return AvailableHumans >= requiredAmount; 
    }

    public override void Upgrade()
    {
        base.Upgrade();

        // TODO: increase amount of total and available humans based on the curve
    }
}
