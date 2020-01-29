using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            _columns.Sort();

            float minDistance = Vector3.Distance(_columns[0].transform.position, _columns[1].transform.position);
            int firstNearColumnIndex = 0;

            for (int i = 1; i < _columns.Count - 1; i++)
            {
                float distance = Vector3.Distance(_columns[i].transform.position, _columns[i + 1].transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    firstNearColumnIndex = i;
                }
            }

            return new int[]{firstNearColumnIndex, firstNearColumnIndex + 1 };
        }
        else
        {
            return new int[] { 0, 1};
        }
    }

}
