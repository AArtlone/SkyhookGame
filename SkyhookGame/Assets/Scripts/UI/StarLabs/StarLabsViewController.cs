using MyUtilities.GUI;
using System.Collections.Generic;
using UnityEngine;

public class StarLabsViewController : ViewController
{
    [SerializeField] private StarLabsSelectableController selectableController = default;
    [SerializeField] private StarLabsTabGroup tabGroup = default;

    private void TabGroup_OnTabSelection(StudyType type)
    {
        var studiesToShow = DSModelManager.Instance.StudiesSO.GetStudiesByType(type);

        SetStudiesDataSet(studiesToShow);
    }

    public override void ViewWillAppear()
    {
        base.ViewWillAppear();

        tabGroup.onTabSelection += TabGroup_OnTabSelection;
    }

    public override void ViewWillDisappear()
    {
        base.ViewWillDisappear();

        tabGroup.onTabSelection -= TabGroup_OnTabSelection;
    }

    private void SetStudiesDataSet(List<Study> studies)
    {
        List<StudyCellData> dataSet = new List<StudyCellData>(studies.Count);

		for (int i = 1; i < studies.Count; i++)
		{
			dataSet.Add(new StudyCellData(studies[i]));
		}

        selectableController.SetDataSet(dataSet);
    }
}
