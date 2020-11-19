using MyUtilities.GUI;
using UnityEngine;

public class ManufactoryTasksSelectableController : 
    SelectableController<ManufactoryTasksCell, ManufactoryTasksCellData>
{
    [SerializeField] private GameObject noTasksText = default;

    protected override void Refresh()
    {
        noTasksText.SetActive(CheckIfTasksAreEmpty());

        base.Refresh();
    }

    private bool CheckIfTasksAreEmpty()
    {
        return Manufactory.ManufactoryTasks.Count == 0;
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
