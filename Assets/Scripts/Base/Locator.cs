using UnityEngine;

public class Locator : MonoBehaviour
{
    [SerializeField] private Plane _plane;

    public Resource Scan()
    {
        return _plane.GetFreeResource();
    }
}
