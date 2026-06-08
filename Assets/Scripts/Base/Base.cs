using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Locator _locator;
    [SerializeField] private List<Unit> _units;

    private Resource _targetResource;

    private void OnEnable()
    {
        foreach (var unit in _units)
        {
            unit.Freed += GivePosition;
        }
    }

    private void Start()
    {
        foreach (var unit in _units)
        {
            GivePosition(unit);
        }
    }

    private void OnDisable()
    {
        foreach (var unit in _units)
        {
            unit.Freed += GivePosition;
        }
    }

    private void GivePosition(Unit unit)
    {
        _targetResource = _locator.Scan();

        if (_targetResource == null)
            return;

        unit.TakeDirection(_targetResource);
    }
}