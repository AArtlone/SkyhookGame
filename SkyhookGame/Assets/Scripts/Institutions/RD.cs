using System.Collections.Generic;

public class RD : Institution
{
    private List<Study> availableStudies;
    private List<Study> studiesInProgress;

    private int tasksCapacity;

    public override void Upgrade()
    {
        base.Upgrade();

        // Increase tasks capacity based on the animation curve
    }
}
