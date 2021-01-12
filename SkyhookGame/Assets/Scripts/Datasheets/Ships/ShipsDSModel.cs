using MyUtilities.DataSheets;
using System.Collections.Generic;

public class ShipsDSModel: DSModelBase<ShipsDSRecord, ShipsDSID>
{
	private const float SkyhookFuelReducage = .86f;

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
				bool containReqStudy = StudiesManager.Instance.CompletedStudies.Contains(shipRecord.reqStudy);
				if (!containReqStudy)
					continue;
            }

			result.Add(new ShipRecipe(shipRecord.recordID, shipRecord.shipName));
		}

		return result;
	}

	public int GetReqFuel(ShipsDSID shipID, bool viaSkyhook)
    {
		foreach (var record in GetAllRecords())
        {
			if (record.recordID == shipID)
            {
				if (!viaSkyhook)
					return record.maxFuel;
				else 
				{
					int reduced = (int)(record.maxFuel * SkyhookFuelReducage);
					return record.maxFuel - reduced;
				}
			}
        }

		return 0;
    }
}
