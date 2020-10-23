using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Institution
{
    private int storageCapacity;

    public override void Upgrade()
    {
        base.Upgrade();

        //TODO: increase storage capacity based on the animation curve
    }
}
