using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoubleTap : MonoBehaviour
{
    public Action onDoubleTap;

    [SerializeField] private LayerMask layerMask = default;
    [SerializeField] private GraphicRaycaster graphicRaycaster = default;

    private float touchDuration;
    private Touch touch;
    private EventSystem eventSystem;

    private int clickCount;

    private void Awake()
    {
        if (graphicRaycaster == null)
        {
            Debug.LogError("GraphicRaycaster is not assigned in the editor");
            enabled = false;
            return;
        }

        eventSystem = FindObjectOfType<EventSystem>();
        onDoubleTap += OnDoubleTap;
    }

    private void OnDestroy()
    {
        onDoubleTap -= OnDoubleTap;
    }

    private void OnDoubleTap()
    {
        PlanetsOverviewUIManager.Instance.ShowSolarSystemView();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            HandleEditorInput();
            return;
        }

        HandleTouchInput();
    }

	private void HandleEditorInput()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            touchDuration = 0f;
            return;
        }

        // If we hit any UI then we do not need to make any further checks
        if (HitUI(Input.mousePosition))
            return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hits = Physics2D.RaycastAll(ray.origin, ray.direction, 100f, layerMask);

        if (hits.Length != 1)
			return;

        clickCount++;
        touchDuration += Time.deltaTime;

        if (clickCount == 1 && touchDuration < 0.2f)
            StartCoroutine(SingleOrDoubleEditor());
    }

	private void HandleTouchInput()
    {
        if (Input.touchCount <= 0)
        {
            touchDuration = 0f;
            return;
        }

        for (int i = 0; i < Input.touchCount; i++)
		{
			var touch = Input.GetTouch(i);

			if (touch.phase != TouchPhase.Began)
				continue;

            // If we hit any UI then we do not need to make any further checks
            if (HitUI(touch.position))
                return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics2D.GetRayIntersectionAll(ray, 100f, layerMask);

			if (hits.Length != 1)
				return;

            touchDuration += Time.deltaTime;
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended && touchDuration < 0.2f)
                StartCoroutine(SingleOrDoubleTouch());

            Debug.Log("Clicking on DoubleTapArea");
		}
    }

    private bool HitUI(Vector2 inputPos)
    {
        List<RaycastResult> results = new List<RaycastResult>();

        var pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = inputPos;

        graphicRaycaster.Raycast(pointerEventData, results);

        return results.Count != 0;
    }

    // TODO: test this on mobile device
    private IEnumerator SingleOrDoubleTouch()
    {
        yield return new WaitForSeconds(0.3f);
        if (touch.tapCount == 1)
            Debug.Log("Single");
        else if (touch.tapCount == 2)
        {
            //this coroutine has been called twice. We should stop the next one here otherwise we get two double tap
            StopCoroutine(SingleOrDoubleTouch());
            Debug.Log("Double");
        }
    }

    private IEnumerator SingleOrDoubleEditor()
    {
        yield return new WaitForSeconds(0.3f);
        
        if (clickCount == 2)
            onDoubleTap?.Invoke();

        clickCount = 0;
    }
}
