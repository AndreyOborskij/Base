using UnityEngine;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private Base _mainBase;
    [SerializeField] private Base _prefabBase;
    [SerializeField] private ResourceDatabase _resourceDatabase;

    private int _costCreate = 5;
    private Base _newBase;

    public int CostCreate => _costCreate;

    private void Awake()
    {
        _mainBase.InitBuilder(this, _resourceDatabase, Build);
    }

    private void Build(Base mainBase, Vector3 position, Unit unit)
    {
       _newBase = Instantiate(_prefabBase, position, Quaternion.identity);

        Exchange(mainBase, _newBase, unit);
    }

    private void Exchange(Base mainBase, Base newBase, Unit unit)
    {
        newBase.InitBuilder(this, _resourceDatabase, Build);
        mainBase.RemoveUnit(unit);
        mainBase.BuildComplete();
        newBase.AddUnit(unit);
    }
}
