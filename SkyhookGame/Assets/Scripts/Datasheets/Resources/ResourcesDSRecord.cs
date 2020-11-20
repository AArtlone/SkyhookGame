using MyUtilities.DataSheets;
using System;

[Serializable]
public class ResourcesDSRecord: DSRecordBase<ResourcesDSID>
{
	public int oneUnitMass;

	public ResourcesDSRecord(string[] csvFileLine)
	{
		recordID = (ResourcesDSID)Enum.Parse(typeof(ResourcesDSID), csvFileLine[0]);
		oneUnitMass = int.Parse(csvFileLine[1]);
	}
}
