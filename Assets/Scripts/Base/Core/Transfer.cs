using UnityEngine;

public class Transfer : MonoBehaviour
{
    [SerializeField] private BaseBuilder _builder;
    [SerializeField] private ResourceDatabase _resourceDatabase;

    private void OnEnable()
    {
        _builder.Built += Exchange;
    }

    private void OnDisable()
    {
        _builder.Built -= Exchange;
    }

    public void Exchange(Base mainBase, Base newBase, Unit unit)
    {
        newBase.InitBuilder(_builder, _resourceDatabase);
        mainBase.RemoveUnit(unit);
        mainBase.BuildComplete();
        newBase.AddUnit(unit);
    }
}
