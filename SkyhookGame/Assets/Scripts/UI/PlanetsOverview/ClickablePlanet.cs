using MyUtilities.GUI;
using UnityEngine;

[RequireComponent(typeof(MyButton))]
public class ClickablePlanet : MonoBehaviour
{
    [SerializeField] private Planet planet = default;
    
    private MyButton myButton = default;

    public Planet Planet { get { return planet; } }

    private void Awake()
    {
        myButton = GetComponent<MyButton>();
        myButton.onClick += MyButton_OnClick;
    }

    private void MyButton_OnClick()
    {
        PlanetsOverviewUIManager.Instance.PlanetsOverviewViewController.SelectPlanet(planet);
    }
}

public enum Planet
{
    Earth,
    Moon,
    Mars
}
