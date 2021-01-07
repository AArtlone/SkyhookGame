using MyUtilities.DataSheets;
using System.Collections.Generic;

public class ShipsDSModel: DSModelBase<ShipsDSRecord, ShipsDSID>
{
	protected override ShipsDSRecord CreateRecord(string[] csvFileLine)
	{
		var result = new ShipsDSRecord(csvFileLine);

		return result;
	}

	public List<ShipRecipe> GetShipRecipes()
    {
		var shipRecords = GetAllRecords();

		List<ShipRecipe> result = new List<ShipRecipe>(shipRecords.Count);

        foreach (ShipsDSRecord shipRecord in shipRecords)
		{
			if (shipRecord.reqStudy != StudyCode.None)
            {
				if (!StudiesManager.Instance.CompletedStudies.Contains(shipRecord.reqStudy))
					continue;
            }

			result.Add(new ShipRecipe(shipRecord.recordID, shipRecord.shipName, shipRecord.price, shipRecord.mass, shipRecord.reqAluminium, shipRecord.reqPlatinum));
		}

		return result;
	}
}
