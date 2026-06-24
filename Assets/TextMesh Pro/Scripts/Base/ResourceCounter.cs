using TMPro;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countInfo;
    [SerializeField] private Storage _storage;

    private void OnEnable()
    {
        _storage.Took += CountInfo;
    }

    private void OnDisable()
    {
        _storage.Took -= CountInfo;
    }

    private void CountInfo(int count)
    {
        _countInfo.text = $"{count}";
    }
}
