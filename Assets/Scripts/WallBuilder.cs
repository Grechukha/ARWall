using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    [SerializeField] private Wall _wallPrefab;
    [SerializeField] private float _deltaPosition;

    private Wall _wall;
    private Vector3[] _lastColumnsPositions = null;

    public Wall BuildBetween(Column column1, Column column2)
    {
        if (_wall == null)
        {
            if (IsInstantiateNeeded(column1, column2))
            {
                _lastColumnsPositions = null;

                _wall = Instantiate(_wallPrefab);
                _wall.SetColumns(column1, column2);
                _wall.Died += OnWallDied;
            }
        }
        else
        {
            if (IsRebuildNeeded(column1, column2))
            {
                _wall.SetColumns(column1, column2);
            }
        }
        return _wall;
    }

    public void Remowe()
    {
        if (_wall != false)
        {
            _wall.Died -= OnWallDied;

            Destroy(_wall.gameObject);
            _wall = null;
        }
    }

    private void OnWallDied(Vector3[] lastColumnsPositions)
    {
        _lastColumnsPositions = lastColumnsPositions;

        Remowe();
    }

    private bool IsInstantiateNeeded(Column column1, Column column2)
    {
        if (_lastColumnsPositions == null)
        {
            return true;
        }
        else
        {
            float deltaDistanceFirstColumn = Vector3.Distance(column1.transform.position, _lastColumnsPositions[0]);
            float deltaDistanceSecondColumn = Vector3.Distance(column2.transform.position, _lastColumnsPositions[1]);

            return (deltaDistanceFirstColumn > _deltaPosition || deltaDistanceSecondColumn > _deltaPosition);
        }
    }

    private bool IsRebuildNeeded(Column column1, Column column2)
    {
        Column firstColumn = null;
        Column secondColumn = null;

        if (column1.GetHashCode() == _wall.FirstColumnHashCode)
            firstColumn = column1;
        if (column1.GetHashCode() == _wall.SecondColumnHashCode)
            secondColumn = column1;
        if (column2.GetHashCode() == _wall.FirstColumnHashCode)
            firstColumn = column2;
        if (column2.GetHashCode() == _wall.SecondColumnHashCode)
            secondColumn = column2;

        if(firstColumn == null || secondColumn == null)
        {
            return true;
        }
        else
        {
            float deltaDistanceFirstColumn = Vector3.Distance(firstColumn.transform.position, _wall.BuildedFirstColumnPosition);
            float deltaDistanceSecondColumn = Vector3.Distance(secondColumn.transform.position, _wall.BuildedSecondColumnPosition);

            return (deltaDistanceFirstColumn > _deltaPosition || deltaDistanceSecondColumn > _deltaPosition);
        }
    }
}
