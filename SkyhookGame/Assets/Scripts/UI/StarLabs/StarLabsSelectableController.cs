using MyUtilities.GUI;
using System;

public class StarLabsSelectableController : SelectableController<StudyCell, StudyCellData>
{
	private SelectableCell<StudyCellData> pressedStudyCell;

    protected override void Cell_OnCellPress(SelectableCell<StudyCellData> cell)
    {
        base.Cell_OnCellPress(cell);
		pressedStudyCell = cell;

		print(cell.data.study.studyType);
        print(cell.data.study.GetCode());

        CreateUnlockPopUp(cell.data.study);
    }

    private void CreateUnlockPopUp(Study study)
    {
        if (StudiesManager.Instance.CheckIfStudyIsUnlocked(study.GetCode()))
        {
            CreateCannotUnlockPopUp();
            return;
        }

        string text = $"Do you want to unlock {study.GetCode()} study?";
        string button1Text = "Yes";
        string button2Text = "No";
        Action button1Callback = new Action(() =>
        {
			StudiesManager.Instance.UnlockStudy(
				(StudyCode)Enum.Parse(typeof(StudyCode),
				study.GetCode())
			);

			pressedStudyCell.Refresh();
		});

        PopUpManager.CreateDoubleButtonTextPopUp(text,
            button1Text,
            button2Text,
            button1Callback);
    }

    private void CreateCannotUnlockPopUp()
    {

        string text = $"This study is already unlocked.";
        string button1Text = "Ok";

        PopUpManager.CreateSingleButtonTextPopUp(text,
            button1Text,
            new Action(() => { }));
    }
}
