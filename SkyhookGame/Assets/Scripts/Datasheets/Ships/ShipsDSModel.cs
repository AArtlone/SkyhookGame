using MyUtilities.DataSheets;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Model", menuName="Datasheets/Model")]
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
			result.Add(new ShipRecipe(shipRecord.recordID, shipRecord.shipName, shipRecord.price));

		return result;
	}
}
