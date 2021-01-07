using MyUtilities.GUI;
using System.Collections.Generic;
using UnityEngine;

public class SkyhookContainer : BaseTouchController
{
    private const string LayerName = "SkyhookContainer";
    [SerializeField] private List<GameObject> blockingObjects = default; // A list of UI objects that should block raycast

    [Space(5f)]
    [SerializeField] private bool facingRightSideOfTheScreen = default;

    [Space(5f)]
    [SerializeField] private GameObject blueprint = default;
    
    [Space(5f)]
    [SerializeField] private Vector3 startTrackingVector = default;
    [SerializeField] private Vector3 endTrackingVector = default;

    public GameObject Blueprint { get { return blueprint; } }
    public Vector3 StartTrackingVector { get { return startTrackingVector;} }
    public Vector3 EndTrackingVector { get { return endTrackingVector; } }
    public bool FacingRightSideOfTheScreen { get { return facingRightSideOfTheScreen; } }

    private LayerMask layerMask = default;

    protected override void Awake()
    {
        if (blueprint == null)
        {
            Debug.LogWarning($"Preview is not assigned in the editor on {gameObject.name} gameobject");
            enabled = false;
        }

        base.Awake();

        layerMask = 1 << LayerMask.NameToLayer(LayerName);
    }

    public void DisableBlueprint()
    {
        blueprint.SetActive(false);
    }

    protected override void Update()
    {
        if (InstitutionsUIManager.Instance.IsUIDisplayed) return;

        foreach (var obj in blockingObjects)
            if (obj.activeSelf)
                return;

        if (Application.isEditor)
        {
            HandleEditorInput();
            return;
        }

        HandleTouchInput();
    }

    protected override LayerMask GetLayerMask()
    {
        return layerMask;
    }

    protected override int GetLayerIndex()
    {
        return LayerMask.NameToLayer(LayerName);
    }

    protected override void OnTouch()
    {
        int skyhooksInStorage = Settlement.Instance.Manufactory.SkyhooksInStorage;

        if (skyhooksInStorage == 0)
            ShowNoSkyhooksPopUp();
        else
            SkyhookManager.Instance.SpawnSkyhook(this);
    }

    private void ShowNoSkyhooksPopUp()
    {
        var description = "There are no Skyhooks in the storage";

        var buttonText = "Ok";

        PopUpManager.CreateSingleButtonTextPopUp(description, buttonText);
    }
}