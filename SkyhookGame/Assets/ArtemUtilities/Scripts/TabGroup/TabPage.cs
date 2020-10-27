using UnityEngine;

public class TabPage : MonoBehaviour
{
    public virtual void ShowPage()
    {
        gameObject.SetActive(true);
    }
}
