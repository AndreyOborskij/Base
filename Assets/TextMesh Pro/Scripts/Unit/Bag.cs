using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private Transform _carryPoint;

    private Resource _carriedResource;

    public Resource CarriedResource => _carriedResource;
    public Transform CarryPoint => _carryPoint;

    public void PutResource(Resource resource)
    {
        _carriedResource = resource;
    }

    public void Clear()
    {
        _carriedResource = null; 
    }
}
