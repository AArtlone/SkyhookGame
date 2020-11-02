using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    private List<ViewController> stack = new List<ViewController>();

    private Queue<NavigationQueueElement> queue = new Queue<NavigationQueueElement>();

    private bool isInTransition;

    public void DebugStack()
    {
        print(stack.Count);
        foreach (var v in stack)
        {
            print(v.gameObject.name);
        }
    }

    public void Push(ViewController newViewController, bool hidePreviousView)
    {
        if (isInTransition)
        {
            queue.Enqueue(new NavigationQueueElement(NavigationCommand.Push));

            Debug.Log($"{newViewController.gameObject.name} is beind added to queue with {NavigationCommand.Push} command");
            
            Debug.LogWarning("Is already in transition");
            return;
        }
        
        if (newViewController == null)
        {
            Debug.LogWarning($"{newViewController.name} view controller is null");
            return;
        }

        if (stack.Contains(newViewController))
        {
            Debug.LogWarning($"The stack already contains the {newViewController.name}");
            return;
        }

        isInTransition = true;

        StartCoroutine(PushViewControllerCo(newViewController, hidePreviousView));
    }

    private IEnumerator PushViewControllerCo(ViewController newViewController, bool hidePreviousView)
    {
        ViewController topViewController = GetTopViewController();

        if (topViewController != null && hidePreviousView)
        {
            topViewController.ViewWillDisappear();
            
            // Check if there is a transition time when using Effects

            yield return null;

            topViewController.gameObject.SetActive(false);

            topViewController.Disappeared();
        }

        stack.Add(newViewController);

        newViewController.ViewWillAppear();

        yield return null;

        // Check if there is a transition time when using Effects
        
        newViewController.gameObject.SetActive(true);

        isInTransition = false;

        newViewController.ViewAppeared();
    }

    public void PushAndPop(ViewController newViewController)
    {
        if (isInTransition)
        {
            queue.Enqueue(new NavigationQueueElement(NavigationCommand.PushAndPop));

            Debug.Log($"{newViewController.gameObject.name} is beind added to queue with {NavigationCommand.PushAndPop} command");

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
            topViewController.ViewWillDisappear();

            // Check if there is a transition time when using Effects

            yield return null;

            topViewController.gameObject.SetActive(false);

            topViewController.Disappeared();

            stack.Remove(topViewController);
        }

        stack.Add(newViewController);

        newViewController.ViewWillAppear();

        yield return null;

        // Check if there is a transition time when using Effects

        newViewController.gameObject.SetActive(true);

        isInTransition = false;

        newViewController.ViewAppeared();
    }

    public void PopTopViewController()
    {
        if (stack.Count == 0)
        {
            Debug.LogWarning("Stack is empty");
            return;
        }

        if (isInTransition)
        {
            queue.Enqueue(new NavigationQueueElement(NavigationCommand.Pop));

            Debug.Log($"{NavigationCommand.Pop} command is beind added to queue with.");

            Debug.LogWarning("Is already in transition");
            return;
        }

        isInTransition = true;

        StartCoroutine(PopTopViewControllerCo());
    }

    private IEnumerator PopTopViewControllerCo()
    {
        ViewController topViewController = GetTopViewController();

        if (topViewController == null)
            yield break;

        topViewController.ViewWillDisappear();

        // Check if there is a transition time when using Effects

        yield return null;

        topViewController.gameObject.SetActive(false);

        topViewController.Disappeared();

        stack.Remove(topViewController);

        isInTransition = false;
    }

    private void HandleQueue()
    {

    }

    private ViewController GetTopViewController()
    {
        if (stack.Count == 0)
            return null;

        return stack[stack.Count - 1];
    }
}

public class NavigationQueueElement
{
    public NavigationCommand navCommand;
    public NavigationQueueElement (NavigationCommand navCommand)
    {
        this.navCommand = navCommand;
    }
}

public enum NavigationCommand
{
    Push,
    PushAndPop,
    Pop
}
