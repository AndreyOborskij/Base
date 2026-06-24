using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Locator _locator;
    [SerializeField] private ResourceDatabase _resourceDatabase;
    [SerializeField] private List<Unit> _units;

    private void OnEnable()
    {
        foreach (Unit unit in _units)
        {
            unit.Freed += GiveTask;
        }
    }

    private void Start()
    {
        foreach (Unit unit in _units)
        {
            GiveTask(unit);
        }
    }

    private void OnDisable()
    {
        foreach (Unit unit in _units)
        {
            unit.Freed -= GiveTask;
        }
    }

    private void GiveTask(Unit unit)
    {
        List<Resource> foundResources = _locator.Scan();
        _resourceDatabase.UpdateResources(foundResources);

        Resource resource = _resourceDatabase.GetFreeResource();

        if (resource == null)
            return;

        unit.TakeDirection(resource);
    }
}