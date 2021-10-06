using MyUtilities.DataSheets;
using System;

[Serializable]
public class ShipsDSRecord: DSRecordBase<ShipsDSID>
{
	public string shipName;
	public int maxFuel;
	public int maxNonFuel;
	public StudyCode reqStudy;

	public ShipsDSRecord(string[] csvFileLine)
	{
		recordID = (ShipsDSID)Enum.Parse(typeof(ShipsDSID), csvFileLine[0]);
		shipName = csvFileLine[1];
		maxFuel = int.Parse(csvFileLine[2]);
		maxNonFuel = int.Parse(csvFileLine[3]);
		reqStudy = (StudyCode)Enum.Parse(typeof(StudyCode), csvFileLine[4]);
	}
}
