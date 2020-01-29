using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    [SerializeField] private Wall _wallPrefab;
    private Wall _wall;

    public Wall BuildBetween(Column column1, Column column2)
    {
        Vector3 position = column1.transform.position + (column2.transform.position - column1.transform.position) / 2;
        float length = Vector3.Distance(column1.transform.position, column2.transform.position);

        if (_wall == null)
        {
            _wall = Instantiate(_wallPrefab, position, column1.transform.rotation);
        }
        else
        {
            _wall.transform.position = position;
        }

        _wall.transform.localScale = new Vector3(_wall.transform.localScale.x, _wall.transform.localScale.y, length);
        _wall.transform.LookAt(column1.transform);

        return _wall;
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
