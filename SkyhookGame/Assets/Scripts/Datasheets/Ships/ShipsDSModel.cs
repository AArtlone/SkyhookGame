using MyUtilities.DataSheets;
using System.Collections.Generic;
using UnityEngine;

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

        foreach (var shipRecord in shipRecords)
			result.Add(new ShipRecipe(shipRecord.recordID, shipRecord.shipName, shipRecord.price, shipRecord.mass, shipRecord.reqAluminium, shipRecord.reqPlatinum));

		return result;
	}
}
