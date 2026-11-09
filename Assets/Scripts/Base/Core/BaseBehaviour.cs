using System;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    [SerializeField] private FlagController _flagController;

    private BaseState _currentStates = BaseState.CreateUnit;

    private void OnEnable()
    {
        _flagController.FlagPlaced += ChangeState;
    }

    private void Start()
    {
        StateChanged?.Invoke(_currentStates);
    }

    private void OnDisable()
    {
        _flagController.FlagPlaced -= ChangeState;
    }

    public event Action<BaseState> StateChanged;

    public void ChangeState(BaseState state)
    {
        _currentStates = state;

        StateChanged?.Invoke(_currentStates);
    }
}
