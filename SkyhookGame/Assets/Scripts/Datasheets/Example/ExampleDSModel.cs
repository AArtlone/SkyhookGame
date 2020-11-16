using UnityEngine;

[CreateAssetMenu(fileName="Model", menuName="Datasheets/Model")]
public class ExampleDSModel: DSModelBase<ExampleDSRecord, ExampleDSID>
{
	protected override ExampleDSRecord CreateRecord(string[] csvFileLine)
	{
		var result = new ExampleDSRecord(csvFileLine);

		return result;
	}
}
