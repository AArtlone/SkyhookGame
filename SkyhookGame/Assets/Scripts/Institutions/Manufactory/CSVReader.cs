using System.Collections.Generic;
using UnityEngine;

class CSVReader : MonoBehaviour
{
    private List<DatasheetRecord> allRecords = new List<DatasheetRecord>();

    void Start()
    {
        TextAsset DataCSV = Resources.Load<TextAsset>("Test");

        string[] fileText = DataCSV.text.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);

        // We start with i = 1, because first line of the csv file is always a desciption line and does not need to be taken into account
        // We interate until length - 1, because last line is always empty in the csv file
        for (int i = 1; i < fileText.Length - 1; i++)
        {
            string[] line = fileText[i].Split(new char[] { ';' });

            bool skipLine = false;
            for (int j = 0; j < line.Length; j++)
            {
                if (string.IsNullOrEmpty(line[j]))
                {
                    print("Line contain empty fields");
                    skipLine = true;
                    break;
                }
            }

            if (skipLine)
                continue;

            CosmicPortLevelUpID id = (CosmicPortLevelUpID)System.Enum.Parse(typeof(CosmicPortLevelUpID), line[0]);
            StudyType studyType = (StudyType)System.Enum.Parse(typeof(StudyType), line[1]);

            int reqStudyLevel = int.Parse(line[2]);

            int reqExp = int.Parse(line[3]);

            var record = new DatasheetRecord(id, studyType, reqStudyLevel, reqExp);

            allRecords.Add(record);
        }

        foreach (var v in allRecords)
            print($"{v.id} | {v.studyType} | {v.reqStudyLvl} | {v.reqExp}");
    }

    public class DatasheetRecord
    {
        public CosmicPortLevelUpID id;
        public StudyType studyType;
        
        public int reqStudyLvl;
        public int reqExp;

        public DatasheetRecord(CosmicPortLevelUpID id, StudyType studyType, int reqStudyLvl, int reqExp)
        {
            this.id = id;
            this.studyType = studyType;

            this.reqStudyLvl = reqStudyLvl;
            this.reqExp = reqExp;
        }
    }

    public enum StudyType
    {
        A,
        B
    }

    public enum CosmicPortLevelUpID
    {
        One,
        Two,
        Three
    }
}