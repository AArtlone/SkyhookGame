using UnityEngine;

public class SettlementBackgroundView : MonoBehaviour
{
    [SerializeField] private GameObject backdrop = default;
    [SerializeField] private GameObject backdropStars = default;

    /// <summary>
    /// Determines the speed in which the background elements rotate.
    /// </summary>
    [Space(10)]
    [Range(1, 10)]
    [SerializeField]
    private int speed = default;

    /// <summary>
    /// Applies a "break" or "boost" to the speed output.
    /// </summary>
    [Range(1, 5)]
    [SerializeField]
    private int multiplier = 3;

    private void Update()
    {
        var newBackdropZ = backdrop.transform.eulerAngles.z + speed * -multiplier * Time.deltaTime;
        backdrop.transform.eulerAngles = new Vector3(0, 0, newBackdropZ);

        var newBackdropStarsZ = backdropStars.transform.eulerAngles.z + speed * -multiplier * .5f * Time.deltaTime;
        backdropStars.transform.eulerAngles = new Vector3(0, 0, newBackdropStarsZ);
    }
}
