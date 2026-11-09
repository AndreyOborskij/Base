using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Base _selectedBase;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectedBase == null)
            {
                ChooseBase();
            }
            else
            {
                ChoosePositionForBuild();
            }

        }
    }

    private void ChooseBase()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out Base mainBase))
            {
                _selectedBase = mainBase;
            }
        }

    }

    private void ChoosePositionForBuild()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out Plane plane))
            {
                _selectedBase.TakePositionForBuild(hit.point);
                _selectedBase = null;
            }
        }
    }
}
