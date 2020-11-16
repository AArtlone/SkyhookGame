using UnityEngine;

public class DSModelManager : Singleton<DSModelManager>
{
    [SerializeField] private ExampleDSModel exampleModel = default;

    public ExampleDSModel ExampleModel { get { return exampleModel; } }

    protected override void Awake()
    {
        SetInstance(this);
    }
}
