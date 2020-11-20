using MyUtilities.DataSheets;
using UnityEngine;

[CreateAssetMenu(fileName="Model", menuName="Datasheets/Model")]
public class ShipsDSModel: DSModelBase<ShipsDSRecord, ShipsDSID>
{
	protected override ShipsDSRecord CreateRecord(string[] csvFileLine)
	{
		var result = new ShipsDSRecord(csvFileLine);

		return result;
	}
}
