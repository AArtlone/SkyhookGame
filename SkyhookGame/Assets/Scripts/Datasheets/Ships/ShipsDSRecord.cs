using MyUtilities.DataSheets;
using System;

[Serializable]
public class ShipsDSRecord: DSRecordBase<ShipsDSID>
{
	public string shipName;
	public int price;
	public int weight;
	public int weightCapacity;
	public int reqAluminium;
	public int reqPlatinum;

	public ShipsDSRecord(string[] csvFileLine)
	{
		recordID = (ShipsDSID)Enum.Parse(typeof(ShipsDSID), csvFileLine[0]);
		shipName = csvFileLine[1];
		price = int.Parse(csvFileLine[2]);
		weight = int.Parse(csvFileLine[3]);
		weightCapacity= int.Parse(csvFileLine[4]);
		reqAluminium = int.Parse(csvFileLine[5]);
		reqPlatinum = int.Parse(csvFileLine[6]);
	}
}
