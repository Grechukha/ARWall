using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private int _life = 10;

    private MeshRenderer _meshRenderer;
    private int _currentLife;
    private float _colorMaxIntensityLength = 3;

    public event Action<Vector3[]> Died;

    public int FirstColumnHashCode { get; private set; }
    public int SecondColumnHashCode { get; private set; }
    public Vector3 BuildedFirstColumnPosition { get; private set; }
    public Vector3 BuildedSecondColumnPosition { get; private set; }

    private void Start()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        _currentLife--;

        if(_currentLife <= 0)
        {
            Died?.Invoke(new Vector3[] {BuildedFirstColumnPosition, BuildedSecondColumnPosition});
        }
    }

    public void SetColumns(Column firstColumn, Column secondColumn)
    {
        FirstColumnHashCode = firstColumn.GetHashCode();
        SecondColumnHashCode = secondColumn.GetHashCode();

        BuildedFirstColumnPosition = firstColumn.transform.position;
        BuildedSecondColumnPosition = secondColumn.transform.position;

        SetParameters(firstColumn, secondColumn);

        _meshRenderer.material.color = GetNewColor();
        _currentLife = _life;
    }

    private void SetParameters(Column firstColumn, Column secondColumn)
    {
        Vector3 position = firstColumn.transform.position + (secondColumn.transform.position - firstColumn.transform.position) / 2;
        float length = Vector3.Distance(firstColumn.transform.position, secondColumn.transform.position);

        transform.position = position;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, length);
        transform.LookAt(firstColumn.transform);
    }

    private Color GetNewColor()
    {
        float colorIntensity = 1 - ((transform.localScale.z - 1) / _colorMaxIntensityLength);

        return new Color(1, colorIntensity, colorIntensity);
    }
}
