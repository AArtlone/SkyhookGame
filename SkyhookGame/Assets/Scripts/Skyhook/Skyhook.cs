using UnityEngine;

public class Skyhook : MonoBehaviour
{
	public bool IsNotOnScreen { get; private set; } = false;
	public bool IsBusy { get; private set; } = false;

	private Vector3 startTrackingVector;
	private Vector3 endTrackingVector;

	private bool rotatingRight;

	private float rotSpeed = 20f;

	private void Update()
	{
		HandleRotation();

		HandleIsNotOnScreenToggle();
	}

	public void Initialize(SkyhookContainer container)
	{
		gameObject.SetActive(true);

		startTrackingVector = container.StartTrackingVector;
		endTrackingVector = container.EndTrackingVector;

		rotatingRight = container.FacingRightSideOfTheScreen;
	}

	private void HandleRotation()
	{
		float rotValue = rotatingRight ? rotSpeed : -rotSpeed;

		transform.Rotate(new Vector3(0, 0, rotValue * Time.deltaTime));
	}

	private void HandleIsNotOnScreenToggle()
	{
		if (!IsNotOnScreen && CheckIfRotationIsEqual(startTrackingVector))
			IsNotOnScreen = true;
		else if (IsNotOnScreen && CheckIfRotationIsEqual(endTrackingVector))
			IsNotOnScreen = false;
	}

	private bool CheckIfRotationIsEqual(Vector3 vectorToCompare)
	{
		float dist = Vector3.Distance(vectorToCompare, transform.up);

		return dist < .1f;
	}

	public void LaunchShip() { }
}
