using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReplayExecutor : IReplay
{
    private Action _forward;
    private Action _backward;

    public ReplayExecutor(Action forward, Action backward)
    {
        _forward = forward;
        _backward = backward;
    }

    public void Forward()
        => _forward();

    public void Backward()
        => _backward();
}
