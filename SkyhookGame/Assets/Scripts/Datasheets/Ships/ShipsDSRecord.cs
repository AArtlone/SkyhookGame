using MyUtilities.DataSheets;
using System;

[Serializable]
public class ShipsDSRecord: DSRecordBase<ShipsDSID>
{
	public int price;
	public int weight;
	public int weightCapacity;

	public ShipsDSRecord(string[] csvFileLine)
	{
		recordID = (ShipsDSID)Enum.Parse(typeof(ShipsDSID), csvFileLine[0]);
		price = int.Parse(csvFileLine[1]);
		weight = int.Parse(csvFileLine[2]);
		weightCapacity= int.Parse(csvFileLine[3]);
	}
}
