using System.Collections.Generic;

public class Production : Institution
{
    private List<Resource> unlockedResources;

    private float incrementalRate;

    public override void Upgrade()
    {
        base.Upgrade();

        //TODO: increase incrementa rate based on the animation curve
    }
}
