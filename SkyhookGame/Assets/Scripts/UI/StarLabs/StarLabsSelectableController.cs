using MyUtilities.GUI;
using System;

public class StarLabsSelectableController : SelectableController<StudyCell, StudyCellData>
{
    protected override void Cell_OnCellPress(SelectableCell<StudyCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        CreateUnlockPopUp(cell.data.study);
    }

    private void CreateUnlockPopUp(Study study)
    {
        string text = $"Do you want to unlock {study.GetCode()} study?";
        string button1Text = "Yes";
        string button2Text = "No";
        Action button1Callback = new Action(() =>
        {
            Settlement.Instance.StarLabs.UnlockStudy(study);
        });

        PopUpManager.CreateDoubleButtonTextPopUp(text,
            button1Text,
            button1Callback,
            button2Text,
            new Action(() => { }));
    }
}
