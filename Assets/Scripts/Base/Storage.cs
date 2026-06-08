using System;
using UnityEngine;
using System.Collections.Generic;

public class Storage : MonoBehaviour
{
    [SerializeField] private Door _door;

    private List<Resource> _allResources;
    private Resource _transferredResource;

    public event Action<int> Took;

    private void Start()
    {
        _allResources = new List<Resource>();
    }

    private void OnEnable()
    {
        _door.Came += TakeResource;
    }

    private void OnDisable()
    {
        _door.Came -= TakeResource;
    }

    private void TakeResource(Unit unit)
    {
        IncreaseValue(unit);
        unit.ReportFreed();
        Took?.Invoke(_allResources.Count);
    }

    private void IncreaseValue(Unit unit)
    {
        _transferredResource = unit.GiveResourse();
        _allResources.Add(_transferredResource);
        _transferredResource.ReportCollection();
    }
}