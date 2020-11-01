using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NavigationController : MonoBehaviour
{
    private List<ViewController> stack = new List<ViewController>();

    private bool isInTransition;

    public void Push(ViewController newViewController)
    {
        if (isInTransition)
        {
            // Add to queue
            Debug.LogWarning("Is already in transition");
            return;
        }
        
        if (newViewController == null)
            return;

        if (stack.Contains(newViewController))
        {
            Debug.LogWarning($"The stack already contains the {newViewController.name}");
            return;
        }

        isInTransition = true;

        StartCoroutine(PushViewControllerCo(newViewController));
    }

    private IEnumerator PushViewControllerCo(ViewController newViewController)
    {
        ViewController topViewController = GetTopViewController();

        if (topViewController != null)
        {
            topViewController.WillDisappear();
            
            // Check if there is a transition time when using Effects

            yield return null;

            topViewController.Disappeared();

            topViewController.gameObject.SetActive(false);
        }

        stack.Add(newViewController);

        newViewController.WillAppear();

        yield return null;

        // Check if there is a transition time when using Effects
        
        newViewController.gameObject.SetActive(true);
        
        newViewController.Appeared();

        isInTransition = false;
    }

    public void PushAndPop(ViewController newViewController)
    {
        if (isInTransition)
        {
            // Add to queue
            Debug.LogWarning("Is already in transition");
            return;
        }

        if (newViewController == null)
            return;

        if (stack.Contains(newViewController))
        {
            Debug.LogWarning($"The stack already contains the {newViewController.name}");
            return;
        }

        isInTransition = true;

        StartCoroutine(PushAndPopViewControllerCo(newViewController));
    }

    private IEnumerator PushAndPopViewControllerCo(ViewController newViewController)
    {
        ViewController topViewController = GetTopViewController();

        if (topViewController != null)
        {
            topViewController.WillDisappear();

            // Check if there is a transition time when using Effects

            yield return null;

            topViewController.Disappeared();

            topViewController.gameObject.SetActive(false);

            stack.Remove(topViewController);
        }

        stack.Add(newViewController);

        newViewController.WillAppear();

        yield return null;

        // Check if there is a transition time when using Effects

        newViewController.gameObject.SetActive(true);

        newViewController.Appeared();

        isInTransition = false;
    }

    private ViewController GetTopViewController()
    {
        if (stack.Count == 0)
            return null;

        return stack[stack.Count - 1];
    }
}
