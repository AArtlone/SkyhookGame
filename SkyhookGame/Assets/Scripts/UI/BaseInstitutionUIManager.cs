using MyUtilities.GUI;
using System.Collections;
using UnityEngine;

public abstract class BaseInstitutionUIManager : MonoBehaviour
{
    [Space(10f)]
    [SerializeField] protected GameObject preview = default;
    [SerializeField] protected GameObject upgradeView = default;

    [SerializeField] protected NavigationController navigationController = default;

    protected virtual void Awake()
    {
        preview.SetActive(false);
        upgradeView.SetActive(false);
    }

    public abstract void Btn_UpgradeInstitution();

    public virtual void Btn_ShowView()
    {
        ToggleIsUIDisplayed(true);
    }

    #region NonOverrideMethods
    public void Btn_ShowUpgradeView()
    {
        preview.SetActive(false);
        upgradeView.SetActive(true);
    }

    private void ToggleIsUIDisplayed(bool value)
    {
        InstitutionsUIManager.Instance.ToggleIsUIDisplayed(value);
    }

    public void Back()
    {
        navigationController.Pop();

        StartCoroutine(CheckIfStackIsEmptyCo());
    }

    private bool IsNavStackEmpty()
    {
        return navigationController.IsNavStackEmpty();
    }

    private IEnumerator CheckIfStackIsEmptyCo()
    {
        yield return new WaitForSeconds(.6f);

        if (IsNavStackEmpty())
            ToggleIsUIDisplayed(false);
    }
    #endregion
}
