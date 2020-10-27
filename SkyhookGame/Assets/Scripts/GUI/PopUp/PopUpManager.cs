using System;
using UnityEngine;

public static class PopUpManager
{
    private static PopUp popOnScreen;

    public static void CreateOKButtonTitleTextPopUp(string title, string text, string buttonText, Action callback)
    {
        CreatePopUp();

        popOnScreen.AddTitle(title);

        popOnScreen.AddDescription(text);

        popOnScreen.AddOneButton(buttonText, callback);
    }

    public static void CreateOKButtonTextPopUp(string text, string buttonText, Action callback)
    {
        CreatePopUp();

        popOnScreen.AddDescription(text);

        popOnScreen.AddOneButton(buttonText, callback);
    }

    public static void CreateDoubleButtonTitleTextPopUp(string title, string text, string firstButtonText, Action firstButtonCallback, string secondButtonText, Action secondButtonCallback)
    {
        CreatePopUp();

        popOnScreen.AddTitle(title);

        popOnScreen.AddDescription(text);

        popOnScreen.AddTwoButtons(firstButtonText, firstButtonCallback, secondButtonText, secondButtonCallback);
    }

    public static void CreateDoubleButtonTextPopUp(string text, string firstButtonText, Action firstButtonCallback, string secondButtonText, Action secondButtonCallback)
    {
        CreatePopUp();

        popOnScreen.AddDescription(text);

        popOnScreen.AddTwoButtons(firstButtonText, firstButtonCallback, secondButtonText, secondButtonCallback);
    }

    private static void CreatePopUp()
    {
        Transform popUpContainer = GameObject.FindGameObjectWithTag("PopUpContainer").transform;

        popOnScreen = UnityEngine.Object.Instantiate(Resources.Load<PopUp>("Prefabs/PopUpPrefab"), popUpContainer);
    }
}
