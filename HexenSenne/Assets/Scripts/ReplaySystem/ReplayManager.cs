using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    private List<IReplay> _replayActions = new List<IReplay>();
    private int _currentAction = -1;

    public bool IsAtEnd => _currentAction >= _replayActions.Count - 1;

    public void Execute(IReplay action)
    {
        _replayActions.Add(action);
        Forward();
    }

    public void Forward()
    {
        if (IsAtEnd)
            return;

        _currentAction++;
        _replayActions[_currentAction].Forward();
    }

    public void Backward()
    {
        if (_currentAction < 0)
            return;

        _replayActions[_currentAction].Backward();
        _currentAction--;
    }
}
