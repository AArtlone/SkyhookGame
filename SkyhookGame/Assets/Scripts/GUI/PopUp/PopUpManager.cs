using System;
using UnityEngine;

public static class PopUpManager
{
    private static PopUp popOnScreen;

    public static void CreateSingleButtonTitleTextPopUp(string title, string text, string buttonText, Action callback)
    {
        CreatePopUp();

        if (popOnScreen == null)
            return;

        popOnScreen.AddTitle(title);

        popOnScreen.AddDescription(text);

        popOnScreen.AddOneButton(buttonText, callback);
    }

    public static void CreateSingleButtonTextPopUp(string text, string buttonText, Action callback)
    {
        CreatePopUp();

        if (popOnScreen == null)
            return;

        popOnScreen.AddDescription(text);

        popOnScreen.AddOneButton(buttonText, callback);
    }

    public static void CreateDoubleButtonTitleTextPopUp(string title, string text, string firstButtonText, Action firstButtonCallback, string secondButtonText, Action secondButtonCallback)
    {
        CreatePopUp();

        if (popOnScreen == null)
            return;

        popOnScreen.AddTitle(title);

        popOnScreen.AddDescription(text);

        popOnScreen.AddTwoButtons(firstButtonText, firstButtonCallback, secondButtonText, secondButtonCallback);
    }

    public static void CreateDoubleButtonTextPopUp(string text, string firstButtonText, Action firstButtonCallback, string secondButtonText, Action secondButtonCallback)
    {
        CreatePopUp();

        if (popOnScreen == null)
            return;

        popOnScreen.AddDescription(text);

        popOnScreen.AddTwoButtons(firstButtonText, firstButtonCallback, secondButtonText, secondButtonCallback);
    }

    private static void CreatePopUp()
    {
        var popUpContainer = GameObject.FindGameObjectWithTag("PopUpContainer");

        if (popUpContainer == null)
        {
            Debug.LogError("PopUpContainer could not been found in the scene. Assing the PopUpContainer tag the container object in the scene");
            return;
        }

        popOnScreen = UnityEngine.Object.Instantiate(Resources.Load<PopUp>("Prefabs/PopUpPrefab"), popUpContainer.transform);
    }
}
