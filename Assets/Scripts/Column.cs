using System;
using UnityEngine;

public class Column : MonoBehaviour , IComparable<Column>
{
    [SerializeField] private GameState _gameState;

    public int CompareTo(Column other)
    {
        float thisDistance = Vector3.Distance(this.transform.position, Vector3.zero);
        float otherDistance = Vector3.Distance(other.transform.position, Vector3.zero);

        return thisDistance.CompareTo(otherDistance);
    }

    public void OnFound()
    {
        _gameState.AddColumn(this);
    }

    public void OnLost()
    {
        _gameState.RemoveColumn(this);
    }
}
