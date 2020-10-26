using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class MyUtilities
{
    public static List<T> ShuffleList<T>(List<T> listToShuffle)
    {
        for (int i = 0; i < listToShuffle.Count; i++)
        {
            var temp = listToShuffle[i];
            int randomIndex = Random.Range(i, listToShuffle.Count);
            listToShuffle[i] = listToShuffle[randomIndex];
            listToShuffle[randomIndex] = temp;
        }
        return listToShuffle;
    }
#if UNITY_EDITOR
    [MenuItem("GameObject/Create Empty At Zero/Game Object", false, -1)]
    public static void CreateGameObjectAtZero()
    {
        var obj = new GameObject("---------------");
        Selection.activeObject = obj;
    }

    [MenuItem("GameObject/Create Empty At Zero/Sprite", false, -1)]
    public static void CreateSpriteAtZero()
    {
        var obj = new GameObject("---------------", typeof(SpriteRenderer));
        Selection.activeObject = obj;
    }


    public static void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
#endif
}
