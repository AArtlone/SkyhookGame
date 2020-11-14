using System.Collections.Generic;
using UnityEngine;

class CSVReader
{
    public static List<string> GetEnumLines(string csvFilePath) 
    {
        List<string> result = new List<string>();

        TextAsset DataCSV = Resources.Load<TextAsset>(csvFilePath);

        string[] fileText = DataCSV.text.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);

        // We start with i = 1, because first line of the csv file is always a desciption line and does not need to be taken into account
        for (int i = 1; i < fileText.Length; i++)
        {
            string[] line = fileText[i].Split(new char[] { ',' });

            result.Add(line[0]);
        }

        return result;
    }
}