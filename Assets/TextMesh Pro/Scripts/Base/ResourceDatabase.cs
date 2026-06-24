using System.Collections.Generic;
using UnityEngine;

public class ResourceDatabase : MonoBehaviour
{
    private List<Resource> _freeResources = new();
    private List<Resource> _busyResources = new();

    public void UpdateResources(List<Resource> foundResources)
    {
        foreach (Resource resource in foundResources)
        {
            if (_freeResources.Contains(resource) == false &&
                _busyResources.Contains(resource) == false)
            {
                _freeResources.Add(resource);
            }
        }
    }

    public Resource GetFreeResource()
    {
        if (_freeResources.Count == 0)
            return null;

        Resource resource = _freeResources[0];

        _freeResources.Remove(resource);
        _busyResources.Add(resource);

        return resource;
    }

    public void RemoveResource(Resource resource)
    {
        _freeResources.Remove(resource);
        _busyResources.Remove(resource);
    }
}
