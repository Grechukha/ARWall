using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(WallSpawner))]
public class GameState : MonoBehaviour
{
    private WallSpawner _wallSpawner;
    private List<Column> _columns = new List<Column>();

    private void Start()
    {
        _wallSpawner = gameObject.GetComponent<WallSpawner>();
    }

    private void Update()
    {
        if (_columns.Count > 1)
        {
            _wallSpawner.SpawnBetween(_columns[0], _columns[1]);
        }
        else
        {
            _wallSpawner.Remowe();
        }
    }

    public void AddColumn(Column column)
    {
        if (_columns.Contains(column) == false)
        {
            _columns.Add(column);
        }
    }

    public void RemoveColumn(Column column)
    {
        if (_columns.Contains(column))
        {
            _columns.Remove(column);
        }
    }
}
