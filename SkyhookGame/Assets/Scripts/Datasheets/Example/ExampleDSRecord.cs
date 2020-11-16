using System;

[Serializable]
public class ExampleDSRecord: DSRecordBase<ExampleDSID>
{
    public string carName;
    public int price;
    public bool availability;

    public ExampleDSRecord(string[] csvFileLine)
    {
        recordID = (ExampleDSID)Enum.Parse(typeof(ExampleDSID), csvFileLine[0]);
        carName = csvFileLine[1];
        price = int.Parse(csvFileLine[2]);
        availability = bool.Parse(csvFileLine[3]);
    }
}
