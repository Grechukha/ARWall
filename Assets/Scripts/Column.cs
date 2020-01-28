using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField] private GameState _gameState;

    public void OnFound()
    {
        _gameState.AddColumn(this);
    }

    public void OnLost()
    {
        _gameState.RemoveColumn(this);
    }
}
