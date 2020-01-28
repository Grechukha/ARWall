using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Wall : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _colorMaxIntensityLength = 3;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _meshRenderer.material.color = GetNewColor();
    }

    private void Update()
    {
        
    }

    private Color GetNewColor()
    {
        float colorIntensity = 1 - ((transform.localScale.z - 1) / _colorMaxIntensityLength);

        return new Color(1, colorIntensity, colorIntensity);
    }
}
