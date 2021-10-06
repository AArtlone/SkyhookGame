using UnityEngine;

public abstract class BaseTouchController : MonoBehaviour
{
    public System.Action onTouch;

    protected abstract LayerMask GetLayerMask();
    protected abstract int GetLayerIndex();

    protected abstract void OnTouch();

    protected abstract void Update();

    protected bool IsInputDisabled { get; private set; }

    protected virtual void Awake()
    {
        onTouch += OnTouch;
    }

    protected void DisableInput()
    {
        IsInputDisabled = true;
    }

    protected void HandleEditorInput()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (!HandleRaycast(Input.mousePosition))
            return;

        onTouch?.Invoke();
    }

    protected void HandleTouchInput()
    {
        if (Input.touchCount <= 0) return;

        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);

            if (touch.phase != TouchPhase.Began)
                continue;

            if (!HandleRaycast(touch.position))
                continue;

            onTouch?.Invoke();
        }
    }

    protected bool HandleRaycast(Vector3 inputPosition)
    {
        var ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit2D[] hits = new RaycastHit2D[1];

        var hitsCount = Physics2D.GetRayIntersectionNonAlloc(ray, hits, 100f, GetLayerMask());

        if (hitsCount == 0)
            return false;

        var hitGameObject = hits[0].collider.gameObject;

        if (hitGameObject.layer != GetLayerIndex())
            return false;

        if (hitGameObject != gameObject)
            return false;

        return true;
    }
}
