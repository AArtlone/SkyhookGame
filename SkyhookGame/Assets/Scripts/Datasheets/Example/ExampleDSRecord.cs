using System;

[Serializable]
public class ExampleDSRecord: DSRecordBase<ExampleDSID>
{
	public ExampleDSRecord(string[] csvFileLine)
	{
		recordID = (ExampleDSID)Enum.Parse(typeof(ExampleDSID), csvFileLine[0]);
	}
}
