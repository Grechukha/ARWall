using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    [SerializeField] private Wall _wallPrefab;
    [SerializeField] private float _deltaPosition;

    private Wall _wall;

    public Wall BuildBetween(Column column1, Column column2)
    {
        if (_wall == null)
        {
            _wall = Instantiate(_wallPrefab);
            _wall.SetColumns(column1, column2);
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

    private bool IsRebuildNeeded(Column column1, Column column2)
    {
        Column firstColumn = null;
        Column secondColumn = null;

        if (column1.GetHashCode() == _wall.FirstColumn.GetHashCode())
            firstColumn = column1;
        if (column1.GetHashCode() == _wall.SecondColumn.GetHashCode())
            firstColumn = column1;
        if (column2.GetHashCode() == _wall.FirstColumn.GetHashCode())
            secondColumn = column2;
        if (column2.GetHashCode() == _wall.SecondColumn.GetHashCode())
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

    public void Remowe()
    {
        if(_wall != false)
        {
            Destroy(_wall.gameObject);

            _wall = null;
        }
    }
}
