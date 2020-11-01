using System.Collections.Generic;
using UnityEngine;

public class ManufactoryViewController : ViewController
{
    [SerializeField] private ManufactoryTabGroup tabGroup = default;

    public override void Appeared()
    {
        tabGroup.Initialize();
    }
}
