using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private SpawnPointContainer _points;
    [SerializeField] private int _maxResources = 4;

    private List<Resource> _resources;
    private List<Resource> _freeResources = new List<Resource>();

    private void Awake()
    {
        _resources = new List<Resource>();

        CreateResources();
    }

    private void OnEnable()
    {
        _spawner.Returned += Recreate;
    }

    private void OnDisable()
    {
        _spawner.Returned -= Recreate;
    }

    public Resource GetFreeResource()
    {
        _freeResources.Clear();

        foreach (Resource resource in _resources)
        {
            if (resource.IsReserved == false)
            {
                _freeResources.Add(resource);
            }
        }

        if (_freeResources.Count == 0)
            return null;

        int index = Random.Range(0, _freeResources.Count);

        Resource target = _freeResources[index];

        target.Reserve();

        return target;
    }

    private void Recreate(Resource resource)
    {
        _points.ReturnPoint(resource.StartPosition);

        _resources.Remove(resource);

        CreateResources();
    }

    private void CreateResources()
    {
        while (_resources.Count < _maxResources)
        {
            Transform point = _points.GetFreePoint();

            if (point == null)
                return;

            Resource resource = _spawner.Spawn(point);

            _resources.Add(resource);
        }
    }
}