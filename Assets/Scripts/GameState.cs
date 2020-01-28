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
            _columns.Sort();

            int firstNearColumnIndex = GetFirstNearColumnIndex();

           _wallSpawner.SpawnBetween(_columns[firstNearColumnIndex], _columns[firstNearColumnIndex + 1]);
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

    private int GetFirstNearColumnIndex()
    {
        if (_columns.Count > 2)
        {
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

            return firstNearColumnIndex;
        }
        else
        {
            return 0;
        }
    }

}
