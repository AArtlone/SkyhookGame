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
				bool containReqStudy = StudiesManager.Instance.CompletedStudies.Contains(shipRecord.reqStudy);
				if (!containReqStudy)
					continue;
            }

			result.Add(new ShipRecipe(shipRecord.recordID, shipRecord.shipName));
		}

		return result;
	}
}
