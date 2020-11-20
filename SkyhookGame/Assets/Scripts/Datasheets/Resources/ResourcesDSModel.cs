using MyUtilities.DataSheets;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Model", menuName = "Datasheets/Model")]
public class ResourcesDSModel : DSModelBase<ResourcesDSRecord, ResourcesDSID>
{
	protected override ResourcesDSRecord CreateRecord(string[] csvFileLine)
	{
		var result = new ResourcesDSRecord(csvFileLine);

		return result;
	}

	public List<ResourcesDSID> GetAllResourcesIDs()
	{
		List<ResourcesDSID> allIDs = new List<ResourcesDSID>(allRecords.Count);

		allRecords.ForEach(r => allIDs.Add(r.recordID));

		return allIDs;
	}

	public int GetOneUnitMass(ResourcesDSID resourceType)
    {
        foreach (var record in allRecords)
        {
			if (record.recordID == resourceType)
				return record.oneUnitMass;
        }

		return 0;
    }
}
