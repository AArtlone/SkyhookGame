using MyUtilities.DataSheets;
using System;

[Serializable]
public class ResourcesDSRecord: DSRecordBase<ResourcesDSID>
{
	public ResourcesDSRecord(string[] csvFileLine)
	{
		recordID = (ResourcesDSID)Enum.Parse(typeof(ResourcesDSID), csvFileLine[0]);
	}
}
