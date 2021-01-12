using MyUtilities.GUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudyCell : SelectableCell<StudyCellData>
{
    [SerializeField] private TextMeshProUGUI code = default;
	[SerializeField] private Image portrait;

    public override void Refresh()
    {
        code.text = data.study.title;

		portrait.sprite = Resources.Load<Sprite>($"UI/Icons/Studies/{data.study.portrait}");
	}
}

public class StudyCellData : SelectableCellData
{
    public Study study;
	public Sprite portrait;

    public StudyCellData(Study study)
    {
        this.study = study;
    }
}
