using System.Collections.Generic;
using UnityEngine;

public class CosmicPortLandLaunchManager
{
    public bool IsLandingOrLaunching { get; private set; }

    private Queue<CosmicPortQueueElement> shipsQueue;

    public CosmicPortLandLaunchManager()
    {
        shipsQueue = new Queue<CosmicPortQueueElement>();
    }

    public void HandleNextQueueElement()
    {
        IsLandingOrLaunching = false;

        if (shipsQueue.Count == 0)
            return;

        var nextElement = shipsQueue.Peek();

        if (nextElement == null)
            return;

        if (nextElement.landOrLaunch == LandOrLaunch.Land)
        {
            Debug.Log("next is land");
        }
        else
        {
            Settlement.Instance.CosmicPort.SendShip(nextElement.dock, nextElement.destination);
            shipsQueue.Dequeue();
        }
    }

    public void AddToQueue(LandOrLaunch landOrLaunch, Dock dock, Planet destination)
    {
        shipsQueue.Enqueue(new CosmicPortQueueElement(landOrLaunch, dock, destination));
    }

    public void SetToBusy()
    {
        IsLandingOrLaunching = true;
    }
}

public class CosmicPortQueueElement
{
    public LandOrLaunch landOrLaunch;
    public Dock dock;
    public Planet destination;

    public CosmicPortQueueElement(LandOrLaunch landOrLaunch, Dock dock, Planet destination)
    {
        this.landOrLaunch = landOrLaunch;
        this.dock = dock;
        this.destination = destination;
    }
}

public enum LandOrLaunch
{
    Land, Launch
}
