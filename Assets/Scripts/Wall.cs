using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Wall : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _colorMaxIntensityLength = 3;

    public Column FirstColumn { get; private set; }
    public Column SecondColumn { get; private set; }
    public Vector3 BuildedFirstColumnPosition { get; private set; }
    public Vector3 BuildedSecondColumnPosition { get; private set; }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetColumns(Column firstColumn, Column secondColumn)
    {
        FirstColumn = firstColumn;
        SecondColumn = secondColumn;

        SetParameters(FirstColumn, SecondColumn);

        BuildedFirstColumnPosition = FirstColumn.transform.position;
        BuildedSecondColumnPosition = SecondColumn.transform.position;

        _meshRenderer.material.color = GetNewColor();
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
