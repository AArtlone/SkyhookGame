using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicPortSkyhookLandLaunchManager
{
    private List<Skyhook> installedSkyhooks;

    public void LaunchShip(Dock launchingDock, Dock destinationDock)
    {
        installedSkyhooks = SkyhookManager.Instance.InstalledSkyhooks;

        foreach (var skyhook in installedSkyhooks)
        {
            if (skyhook.IsNotOnScreen && !skyhook.IsBusy)
            {
               // skyhook.LaunchShip();
                return;
            }
        }
    }
}
