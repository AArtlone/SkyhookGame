using UnityEngine;

public class ManufactoryViewController : ViewController
{
    [SerializeField] private ManufactoryTabGroup tabGroup = default;

    public override void Appeared()
    {
        base.Appeared();

        tabGroup.Initialize();
    }
}
