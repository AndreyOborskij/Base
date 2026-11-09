using System;
using System.Collections.Generic;
using UnityEngine;

public class Headquarters : MonoBehaviour
{
    [SerializeField] private Casern _casern;
    [SerializeField] private Locator _locator;
    [SerializeField] private Storage _storage;
    [SerializeField] private ResourceDatabase _resourceDatabase;
    [SerializeField] private UnitCreator _unitCreator;
    [SerializeField] private BaseBuilder _baseBuilder;
    [SerializeField] private FlagController _flagController;
    [SerializeField] private ResourceReceiver _resourceReceiver;

    private BaseState _currentState;
    private Transform _target;
    private int _minCountUnit = 1;

    public event Action SendBuild; 

    private void OnEnable()
    {
        foreach (Unit unit in _casern.Units)
        {
            unit.Freed += GiveTask;
        }

        _casern.Created += TansferUnitToWork;
    }

    private void Start()
    {
        foreach (Unit unit in _casern.Units)
        {
            GiveTask(unit);
        }
    }

    private void OnDisable()
    {
        foreach (Unit unit in _casern.Units)
        {
            unit.Freed -= GiveTask;
        }

        _casern.Created -= TansferUnitToWork;
    }

    public bool CanBuild => _casern.Units.Count > _minCountUnit;

    public void SetState(BaseState baseState)
    {
        _currentState = baseState;
    }

    public void RemoveUnit(Unit unit)
    {
        unit.Freed -= GiveTask;

        _casern.RemoveUnit(unit);
    }

    public void AddUnit(Unit unit)
    {
        unit.Freed += GiveTask;

        _casern.AddUnit(unit);

        unit.ReportFreed();
    }

    public void Init(BaseBuilder baseBuilder, ResourceDatabase resourceDatabase)
    {
        _baseBuilder = baseBuilder;
        _resourceDatabase = resourceDatabase;
        _resourceReceiver.Init(resourceDatabase);
    }

    public void BuildComplete()
    {
        _flagController.ResetState();
    }

    private void TryCreateUnit()
    {
        if (_storage.TrySpendResources(_unitCreator.CostCreate) == false)
            return;

        _casern.Create();
    }

    private void TryCreateBase(Unit unit)
    {
        if (_storage.TrySpendResources(_baseBuilder.CostCreate) == false)
            return;

        _target = _flagController.FlagPosotion;

        _flagController.DetermineBuilder(unit);

        unit.TakeDirection(_target);

        SendBuild?.Invoke();
    }


    private void TansferUnitToWork(Unit unit)
    {
        unit.Freed += GiveTask;
        GiveTask(unit);
    }

    private void GiveTask(Unit unit)
    {
        if (_currentState == BaseState.CreateUnit)
        {
            TryCreateUnit();
            _target = CollectResources(unit);
        }

        if (_currentState == BaseState.BuildBase)
        {
            if (_storage.CurrentResources < _baseBuilder.CostCreate || _casern.Units.Count <= 1)
            {
                _target = CollectResources(unit);
            }
            else
            {
                TryCreateBase(unit);

                return;
            }
        }

        unit.TakeDirection(_target);
    }

    private Transform CollectResources(Unit unit)
    {
        List<Resource> foundResources = _locator.Scan();

        _resourceDatabase.UpdateResources(foundResources);

        Resource resource = _resourceDatabase.GetFreeResource();

        if (resource == null)
            return null;

        return resource.transform;
    }
}