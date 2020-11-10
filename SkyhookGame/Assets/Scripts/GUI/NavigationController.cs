using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    private List<ViewController> stack = new List<ViewController>();

    private Queue<NavigationQueueElement> queue = new Queue<NavigationQueueElement>();

    private bool isInTransition;

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

    public void Pop()
    {
        print(stack.Count);
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

        ViewController topViewController = GetTopViewController();

        if (topViewController == null)
            return;

        StartCoroutine(PopCo(topViewController));
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

    private IEnumerator PushViewControllerCo(ViewController newViewController)
    {
        UnfocusTopController();

        yield return PushCo(newViewController);
    }

    private IEnumerator PushAndPopCo(ViewController newViewController)
    {
        ViewController topViewController = GetTopViewController();

        if (topViewController != null)
            yield return PopCo(topViewController);

        yield return PushCo(newViewController);
    }

    private IEnumerator PopCo(ViewController topViewController)
    {
        topViewController.ViewWillDisappear(); // View will disappear
        topViewController.ViewWillBeUnfocused(); // View will be unfocused

        yield return topViewController.TransitionOut();

        topViewController.gameObject.SetActive(false);

        isInTransition = false;

        topViewController.ViewDisappeared(); // View has disappeared
        topViewController.ViewUnfocused(); // View has been unfocused

        stack.Remove(topViewController);
    }

    private IEnumerator PushCo(ViewController newViewController)
    {
        stack.Add(newViewController);

        newViewController.ViewWillAppear(); // View will appear
        newViewController.ViewWillBeFocused(); // View will be focused

        newViewController.gameObject.SetActive(true);

        // Performing In Transitions
        yield return newViewController.TransitionIn();

        isInTransition = false;

        newViewController.ViewAppeared(); // View has appeared
        newViewController.ViewFocused(); // View has been focused
    }

    public void FocusTopController()
    {
        ViewController topViewController = GetTopViewController();

        if (topViewController == null)
            return;

        StartCoroutine(FocusTopControllerCo(topViewController));
    }

    private IEnumerator FocusTopControllerCo(ViewController topViewController)
    {
        topViewController.ViewWillBeFocused();

        yield return null;

        topViewController.ViewFocused();
    }

    public void UnfocusTopController()
    {
        ViewController topViewController = GetTopViewController();

        if (topViewController == null)
            return;

        StartCoroutine(UnfocusTopControllerCo(topViewController));
    }

    private IEnumerator UnfocusTopControllerCo(ViewController topViewController)
    {
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

    public void DebugStack()
    {
        print(stack.Count);
        foreach (var v in stack)
        {
            print(v.gameObject.name);
        }
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
