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

        isInTransition = false;

        newViewController.Appeared();
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

        isInTransition = false;

        newViewController.Appeared();
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
    PushAndPop
}
