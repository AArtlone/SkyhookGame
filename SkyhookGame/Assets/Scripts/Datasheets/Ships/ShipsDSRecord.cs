using MyUtilities.DataSheets;
using System;

[Serializable]
public class ShipsDSRecord: DSRecordBase<ShipsDSID>
{
	public string shipName;
	public int price;
	public int mass;
	public int weightCapacity;
	public int reqAluminium;
	public int reqPlatinum;
	public StudyCode reqStudy;

	public ShipsDSRecord(string[] csvFileLine)
	{
		recordID = (ShipsDSID)Enum.Parse(typeof(ShipsDSID), csvFileLine[0]);
		shipName = csvFileLine[1];
		price = int.Parse(csvFileLine[2]);
		mass = int.Parse(csvFileLine[3]);
		weightCapacity= int.Parse(csvFileLine[4]);
		reqAluminium = int.Parse(csvFileLine[5]);
		reqPlatinum = int.Parse(csvFileLine[6]);
		reqStudy = (StudyCode)Enum.Parse(typeof(StudyCode), csvFileLine[7]);
	}
}
