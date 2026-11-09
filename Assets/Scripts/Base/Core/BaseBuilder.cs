using System;
using UnityEngine;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private Base _mainBase;
    [SerializeField] private Base _prefabBase;

    private int _costCreate = 5;
    private Base _newBase;

    public event Action<Base, Base, Unit> Built;

    private void OnEnable()
    {
        _mainBase.FlagController.BuildRequested += Build;
    }

    private void OnDisable()
    {
        _mainBase.FlagController.BuildRequested -= Build;
    }

    public int CostCreate => _costCreate;

    private void Build(Base mainBase, Vector3 position, Unit unit)
    {
       _newBase = Instantiate(_prefabBase, position, Quaternion.identity);

       _newBase.FlagController.BuildRequested += Build;

        Built?.Invoke(mainBase, _newBase, unit);
    }
}
