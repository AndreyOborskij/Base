using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private int _allResources = 0;

    public event Action<int> Took;

    public int CurrentResources => _allResources;

    public void TakeResource()
    {
        _allResources++;        

        Took?.Invoke(_allResources);
    }

    public bool TrySpendResources(int count)
    {
        if(_allResources <  count)
            return false;

        _allResources -= count;
        Took?.Invoke(_allResources);

        return true;
    }
}