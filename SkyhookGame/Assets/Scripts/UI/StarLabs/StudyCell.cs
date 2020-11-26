using MyUtilities.GUI;
using TMPro;
using UnityEngine;

public class StudyCell : SelectableCell<StudyCellData>
{
    [SerializeField] private TextMeshProUGUI code = default;

    public override void Refresh()
    {
        code.text = data.study.title;
    }
}

public class StudyCellData : SelectableCellData
{
    public Study study;

    public StudyCellData(Study study)
    {
        this.study = study;
    }
}
