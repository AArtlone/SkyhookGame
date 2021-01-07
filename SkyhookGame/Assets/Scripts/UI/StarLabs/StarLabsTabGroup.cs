using MyUtilities.GUI;

public class StarLabsTabGroup : TabGroup
{
	public System.Action<StudyType> onTabSelection;

	public override void SelectTab(TabButton tabButton)
	{
		if (selectedTab != null)
			selectedTab.Deselect();

		selectedTab = tabButton;

		ResetTabButtonsVisuals();

		int index = tabButton.transform.GetSiblingIndex();

		StudyType studyType = tabButtons[index].GetComponent<StarLabsTabButton>().Type;

		onTabSelection?.Invoke(studyType);

		if (type == TabGroupType.SpriteBased)
			selectedTab.Select(activeSprite);
		else
			selectedTab.Select(activeColor);
	}
}
