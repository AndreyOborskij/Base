using System;
using UnityEngine;
using System.Collections.Generic;

public class Storage : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private ResourceDatabase _resourceDatabase;

    private List<Resource> _allResources;

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
        if (unit.HasResource == false)
            return;

        Resource resource = unit.GiveResourse();

        _allResources.Add(resource);

        _resourceDatabase.RemoveResource(resource);  

        resource.ReportCollection();

        Took?.Invoke(_allResources.Count);
        unit.ReportFreed();
    }
}