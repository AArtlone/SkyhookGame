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

    public void Push(ViewController newViewController)
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

        StartCoroutine(PushViewControllerCo(newViewController));
    }

    private IEnumerator PushViewControllerCo(ViewController newViewController)
    {
        ViewController topViewController = GetTopViewController();

        if (topViewController != null)
        {
            topViewController.ViewWillBeUnfocused();
            
            // Check if there is a transition time when using Effects

            yield return null;

            topViewController.ViewUnfocused();
        }

        stack.Add(newViewController);

        newViewController.ViewWillBeFocused();

        newViewController.ViewWillAppear();


        yield return null;

        // Check if there is a transition time when using Effects
        
        newViewController.gameObject.SetActive(true);

        yield return newViewController.TransitionIn();

        isInTransition = false;

        newViewController.ViewFocused();

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

        StartCoroutine(PushAndPopCo(newViewController));
    }

    private IEnumerator PushAndPopCo(ViewController newViewController)
    {
        ViewController topViewController = GetTopViewController();

        if (topViewController != null)
        {

            topViewController.ViewWillBeUnfocused();

            topViewController.ViewWillDisappear();

            // Check if there is a transition time when using Effects

            yield return null;

            topViewController.gameObject.SetActive(false);

            topViewController.ViewUnfocused();

            topViewController.Disappeared();

            stack.Remove(topViewController);
        }

        stack.Add(newViewController);

        newViewController.ViewWillBeFocused();

        newViewController.ViewWillAppear();

        yield return null;

        // Check if there is a transition time when using Effects

        newViewController.gameObject.SetActive(true);

        isInTransition = false;

        newViewController.ViewFocused();

        newViewController.ViewAppeared();
    }

    public void Pop()
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

        StartCoroutine(PopCo());
    }

    private IEnumerator PopCo()
    {
        ViewController topViewController = GetTopViewController();

        if (topViewController == null)
            yield break;

        topViewController.ViewWillBeUnfocused();

        topViewController.ViewWillDisappear();

        // Check if there is a transition time when using Effects

        yield return null;

        topViewController.gameObject.SetActive(false);

        topViewController.ViewUnfocused();

        topViewController.Disappeared();

        stack.Remove(topViewController);

        isInTransition = false;
    }

    public void FocusTopController()
    {
        StartCoroutine(FocusTopControllerCo());
    }

    private IEnumerator FocusTopControllerCo()
    {
        var topViewController = GetTopViewController();

        topViewController.ViewWillBeFocused();

        yield return null;

        topViewController.ViewFocused();
    }

    public void UnfocusTopController()
    {
        StartCoroutine(UnfocusTopControllerCo());
    }

    private IEnumerator UnfocusTopControllerCo()
    {
        var topViewController = GetTopViewController();

        topViewController.ViewWillBeUnfocused();

        yield return null;

        topViewController.ViewUnfocused();
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
