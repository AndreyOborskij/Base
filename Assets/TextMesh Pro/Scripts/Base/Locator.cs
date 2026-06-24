using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    private float _scanRadius = 50f;

    public List<Resource> Scan()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

        List<Resource> resources = new();

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Resource resource))
            {
                resources.Add(resource);
            }
        }

        return resources;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}
