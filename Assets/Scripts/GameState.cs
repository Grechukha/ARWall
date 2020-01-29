using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WallBuilder))]
public class GameState : MonoBehaviour
{
    private WallBuilder _wallSpawner;
    private List<Column> _columns = new List<Column>();
    private int[] _selectedColumnsIndices = null;

    private void Start()
    {
        _wallSpawner = gameObject.GetComponent<WallBuilder>();
    }

    private void Update()
    {
        BuildWall();
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

    private void BuildWall()
    {
        if (_columns.Count > 1)
        {
            _selectedColumnsIndices = GetNearColumnsIndices();

            _wallSpawner.BuildBetween(_columns[_selectedColumnsIndices[0]], _columns[_selectedColumnsIndices[1]]);
        }
        else
        {
            _wallSpawner.Remowe();
        }
    }
    
    private int[] GetNearColumnsIndices()
    {
        if (_columns.Count > 2)
        {
            float minDistance = Vector3.Distance(_columns[0].transform.position, _columns[1].transform.position);
            int[] NearColumnsIndices = { 0, 1 };

            for (int i = 0; i < _columns.Count; i++)
            {
                for (int j = i + 1; j < _columns.Count; j++)
                {
                    float distance = Vector3.Distance(_columns[i].transform.position, _columns[j].transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        NearColumnsIndices[0] = i;
                        NearColumnsIndices[1] = j;
                    }
                }
            }

            return NearColumnsIndices;
        }
        else
        {
            return new int[] { 0, 1 };
        }
    }
}
