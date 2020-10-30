using UnityEngine;

public class ManufactorySelectableController : SelectableController<ManufactoryCell, ManufactoryCellData>
{
    [Space(5f)]
    [SerializeField] private BuildShipView buildShipView = default;

    protected override void Cell_OnCellPress(SelectableCell<ManufactoryCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        ShowBuildShipView(cell.data.shipRecipe);
    }

    private void ShowBuildShipView(ShipRecipe shipRecipe)
    {
        buildShipView.ShowView(shipRecipe);
    }

    public void BuildShip()
    {
        bool canBuild = Manufactory.CanBuild();
        bool canStore = Manufactory.CanStore();

        if (!canStore)
        {
            PopUpManager.CreateSingleButtonTextPopUp("The ship cannot be built because there wont be enough space in the storage", "Ok", new System.Action(() => { }));
            return;
        }

        if (!canBuild)
        {
            var description = "The ship cannot be built because you are at your maximum production capacity";

            var buttonText = "Ok";

            var callback = new System.Action(() =>
            {
                print("Hello World");
            });

            PopUpManager.CreateSingleButtonTextPopUp(description, buttonText, callback);
            return;
        }

        buildShipView.gameObject.SetActive(false);

        Manufactory.StartBuildingShip(GetSelectedCell().data.shipRecipe);
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}

