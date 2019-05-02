using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum State { Human, Eagle, Wolf }


class StateManager : MonoBehaviour
{
    static State GameState;

    int _stateCount = 0;

    int _currentState = 0;

    void Start()
    {
        GameState = (State)_currentState;
        _stateCount = Enum.GetValues(typeof(State)).Length;
    }

    public void IncState()
    {
        if (_currentState < _stateCount - 1)
        {
            _currentState++;
        }
        else { _currentState = 0; }

        GameState = (State)_currentState;
    }
}
