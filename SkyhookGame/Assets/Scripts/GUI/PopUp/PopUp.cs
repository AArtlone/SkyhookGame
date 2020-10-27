using System;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public TextMeshProUGUI title = default;
    public TextMeshProUGUI description = default;
    
    public MyButton firstButton = default;
    public MyButton secondButton = default;

    public void AddTitle(string titleText)
    {
        title.gameObject.SetActive(true);
        title.text = titleText;
    }

    public void AddDescription(string text)
    {
        description.gameObject.SetActive(true);
        description.text = text;
    }

    public void AddOneButton(string buttonText, Action callback)
    {
        firstButton.gameObject.SetActive(true);
        firstButton.SetButtonText(buttonText);

        var onClick = new Action(() =>
        {
            callback();

            Destroy(gameObject);
        });

        firstButton.onClick += onClick;
    }

    public void AddTwoButtons(string firstButtonText, Action firstCallback, string secondButtonText, Action secondCallback)
    {
        firstButton.gameObject.SetActive(true);
        firstButton.SetButtonText(firstButtonText);

        var onClickOne = new Action(() =>
        {
            firstCallback();

            Destroy(gameObject);
        });

        firstButton.onClick += onClickOne;

        secondButton.gameObject.SetActive(true);
        secondButton.SetButtonText(secondButtonText);

        var onClickTwo = new Action(() =>
        {
            secondCallback();

            Destroy(gameObject);
        });

        secondButton.onClick += onClickTwo;
    }
}
